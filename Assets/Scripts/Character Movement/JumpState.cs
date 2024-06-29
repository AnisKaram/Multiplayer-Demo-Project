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
        if (m_playerMovementController.isGrounded)
        {
            if (m_playerMovementController.IsWalking()) // walk state
            {
                m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.walkState);
            }
            else // idle state
            {
                m_playerMovementController.playerStateMachine.TransitionTo(m_playerMovementController.playerStateMachine.idleState);
            }
        }
    }
}