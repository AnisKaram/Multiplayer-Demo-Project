using UnityEngine;

public class WalkState : IPlayerState
{
    private PlayerMovementController m_playerMovementController;

    public WalkState(PlayerMovementController player)
    {
        this.m_playerMovementController = player;
    }

    public void Enter() { Debug.Log($"Enter Walk state"); }
    public void Exit() { Debug.Log($"Exit Walk state"); }
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
        if (!m_playerMovementController.IsWalking())
        {
            m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.idleState);
        }
    }
}