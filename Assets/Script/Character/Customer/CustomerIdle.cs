public class CustomerIdle : IState
{
    public void EnterState(StateUser user, StateManager stateManager)
    {
        //  CustomerAI customer = user as CustomerAI;
        // customer.dialogueBubbleUI.SetActive(false);
        // if (customer.isBuying)
        // {
        //     customer.gameObject.SetActive(false);
        // }
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;

        stateManager.SwitchState(customer, customer.walk);
    }
}
