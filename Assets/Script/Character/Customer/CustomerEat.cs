using UnityEngine;

public class CustomerEat : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.isEating = true;   
        customer.plate.transform.parent = customer.table.desk;
        customer.plate.table = customer.table;
        customer.table.isOccupied = true;
        customer.plate.transform.localPosition = Vector3.zero;
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        if (!customer.isEating)
        {
            // customer.speak.Happy();
            customer.plate.EmptyFood();
            customer.plate.isAbleToInteract = true;
            customer.plate = null;
            stateManager.SwitchState(customer, stateManager.walk);
        }
        stateManager.SwitchState(customer, stateManager.walk);
    }
}
