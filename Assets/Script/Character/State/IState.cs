public interface IState
{
    void EnterState(CustomerAI customer, StateManager stateManager);
    void UpdateState(CustomerAI customer, StateManager stateManager);
}
