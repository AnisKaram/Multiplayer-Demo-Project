using UnityEngine;

public class WalkState : IPlayerState
{
    private PlayerMovementController m_playerMovementController;

    public WalkState(PlayerMovementController player)
    {
        this.m_playerMovementController = player;
    }

    public void Enter() { }
    public void Exit() { }
    public void Update()
    {
        // transition to jump
        TryTransitionToJumpState();

        // transition to idle
        TryTransitionToIdleState();
    }

    private void TryTransitionToJumpState()
    {
        if (!m_playerMovementController.isGrounded)
        {
            m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.jumpState);
        }
    }

    private void TryTransitionToIdleState()
    {
        float velocityX = Mathf.Abs(m_playerMovementController.characterController.velocity.normalized.x);
        float velocityZ = Mathf.Abs(m_playerMovementController.characterController.velocity.normalized.z);
        float threshold = m_playerMovementController.movementTransitionThreshold;

        if (velocityX < threshold && velocityZ < threshold)
        {
            m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.idleState);
        }
    }
}