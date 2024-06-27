public class JumpState : IPlayerState
{
    private PlayerMovementController m_playerMovementController;

    public JumpState(PlayerMovementController player)
    {
        this.m_playerMovementController = player;
    }

    public void Enter() { }
    public void Exit() { }
    public void Update()
    {
        // transition to walk
        // transition to idle
    }
}