public class CustomerEat : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.isEating = true;   
        // customer.dialogueBubbleUI.SetActive(false);
        // if (customer.isBuying)
        // {
        //     customer.gameObject.SetActive(false);
        // }

        // to do buat customer duduk dan makan makanan
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        if (!customer.isEating)
        {
            // customer.speak.Happy();
            // to do hapus makanan di piring customer dan buat piring jadi interactable
            stateManager.SwitchState(customer, stateManager.walk);
        }
        stateManager.SwitchState(customer, stateManager.walk);
    }
}
