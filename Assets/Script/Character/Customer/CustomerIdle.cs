public class CustomerIdle : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.dialogueBubbleUI.SetActive(false);
        if (customer.isBuying)
        {
            customer.gameObject.SetActive(false);
        }
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        stateManager.SwitchState(customer, stateManager.walk);
    }
}
