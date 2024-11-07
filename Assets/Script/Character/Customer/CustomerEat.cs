using UnityEngine;

public class CustomerEat : IState
{
    public void EnterState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;

        customer.anim.SetBool("walk",false);
        customer.anim.SetBool("sit",true);
        customer.agent.enabled = false;
        customer.transform.position = customer.table.chair.position;
        
        // customer.transform.LookAt(customer.table.desk);
        Vector3 targetPos = new Vector3( customer.table.desk.position.x,  customer.transform.position.y, customer.table.desk.position.z );
        customer.transform.LookAt(targetPos);
        float posX = customer.transform.position.x;
        float posY = customer.transform.position.y - 0.151998f;
        float posZ = customer.table.desk.position.z < customer.transform.position.z ? customer.transform.position.z + -0.699647f : customer.transform.position.z + 0.440732f;
        customer.transform.position = new Vector3(posX, posY, posZ);

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
            customer.transform.position = customer.table.chair.position;
            customer.anim.SetBool("sit",false);
            // customer.speak.Happy();
            AudioManager.instance.PlaySound(customer.paySound);
            
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
