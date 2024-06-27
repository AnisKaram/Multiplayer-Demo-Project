public interface IPlayerState
{
    void Enter(); // enter state
    void Update(); // transition to new state
    void Exit(); // exit state
}