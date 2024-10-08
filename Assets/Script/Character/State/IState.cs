public interface IState
{
    void EnterState(StateUser user, StateManager stateManager);
    void UpdateState(StateUser user, StateManager stateManager);
}
