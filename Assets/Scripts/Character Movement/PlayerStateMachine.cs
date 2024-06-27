public class PlayerStateMachine
{
    private IPlayerState m_currentState;

    private IdleState m_idleState;
    private WalkState m_walkState;
    private JumpState m_jumpState;

    public IdleState idleState => m_idleState;
    public WalkState walkState => m_walkState;
    public JumpState jumpState => m_jumpState;

    public PlayerStateMachine(PlayerMovementController player)
    {
        this.m_idleState = new IdleState(player);
        this.m_walkState = new WalkState(player);
        this.m_jumpState = new JumpState(player);
    }

    public void Initialize(IPlayerState playerState)
    {
        m_currentState = playerState;
        m_currentState.Enter();
    }
    
    public void TransitionTo(IPlayerState nextPlayerState)
    {
        m_currentState.Exit();

        m_currentState = nextPlayerState;
        m_currentState.Enter();
    }

    public void Update()
    {
        if (m_currentState != null)
        {
            m_currentState.Update();
        }
    }
}