using UnityEngine;

public class CustomerEat : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.agent.enabled = false;
        customer.transform.position = customer.table.chair.position;
        // customer.transform.LookAt(customer.table.desk);
        customer.isEating = true;   
        customer.plate.transform.parent = customer.table.desk;
        customer.plate.table = customer.table;
        customer.table.isOccupied = true;
        customer.plate.transform.localPosition = Vector3.zero;
        CustomerManager.instance.currentCustomer = null;
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        if (!customer.isEating)
        {
            // customer.speak.Happy();
            CurrencyManager.instance.AddCurrency(customer.CountTotalPrice());
            customer.plate.EmptyFood();
            // customer.plate.isAbleToInteract = true;
            customer.plate.gameObject.layer = LayerMask.NameToLayer("interact");
            customer.plate = null;
            customer.agent.enabled = true;
            stateManager.SwitchState(customer, stateManager.walk);
        }
    }
}
