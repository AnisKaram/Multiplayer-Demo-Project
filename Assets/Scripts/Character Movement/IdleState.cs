using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerMovementController m_playerMovementController;

    public IdleState(PlayerMovementController player)
    {
        this.m_playerMovementController = player;
    }

    public void Enter() { Debug.Log($"Enter Idle state"); }
    public void Exit() { Debug.Log($"Exit Idle state"); }
    public void Update()
    {
        // transition to jump
        TryTransitionToJumpState();

        // transition to walk
        TryTransitionToWalkState();
    }

    private void TryTransitionToJumpState()
    {
        if (!m_playerMovementController.isGrounded)
        {
            m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.jumpState);
        }
    }
    private void TryTransitionToWalkState()
    {
        if (m_playerMovementController.IsWalking())
        {
            m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.walkState);
        }
    }
}