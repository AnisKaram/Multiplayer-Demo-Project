using UnityEngine;

public class JumpState : IPlayerState
{
    private PlayerMovementController m_playerMovementController;

    public JumpState(PlayerMovementController player)
    {
        this.m_playerMovementController = player;
    }

    public void Enter() { Debug.Log($"Enter Jump state"); }
    public void Exit() { Debug.Log($"Exit Jump state"); }
    public void Update()
    {
        TransitionToWalkState();

        TransitionToIdleState();
    }

    private void TransitionToWalkState()
    {
        if (m_playerMovementController.isGrounded && m_playerMovementController.IsWalking())
        {
            m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.walkState);
        }
    }

    private void TransitionToIdleState()
    {
        if (m_playerMovementController.isGrounded && !m_playerMovementController.IsWalking())
        {
            m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.idleState);
        }
    }
}