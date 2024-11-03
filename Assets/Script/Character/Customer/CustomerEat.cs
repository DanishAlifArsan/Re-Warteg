using UnityEngine;

public class CustomerEat : IState
{
    public void EnterState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;

        customer.agent.enabled = false;
        customer.transform.position = customer.table.chair.position;
        // customer.transform.LookAt(customer.table.desk);
        Vector3 targetPos = new Vector3( customer.table.desk.position.x,  customer.transform.position.y, customer.table.desk.position.z );
        customer.transform.LookAt(targetPos);
        customer.isEating = true;   
        customer.plate.transform.parent = customer.table.desk;
        customer.plate.table = customer.table;
        customer.table.isOccupied = true;
        customer.plate.transform.localPosition = Vector3.zero;
        CustomerManager.instance.currentCustomer = null;
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;

        if (!customer.isEating)
        {
            // customer.speak.Happy();
            CurrencyManager.instance.AddCurrency(customer.CountTotalPrice());
            CustomerManager.instance.Pickup(customer.CountTotalPrice());
            CustomerManager.instance.profit += customer.CountTotalPrice();
            customer.plate.EmptyFood();
            // customer.plate.isAbleToInteract = true;
            customer.plate.gameObject.layer = LayerMask.NameToLayer("interact");
            customer.plate = null;
            customer.agent.enabled = true;
            stateManager.SwitchState(customer, customer.walk);
        }
    }
}
