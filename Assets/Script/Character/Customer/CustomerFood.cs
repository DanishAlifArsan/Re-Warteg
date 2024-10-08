using UnityEngine;
using UnityEngine.AI;

public class CustomerFood : IState
{
    private Vector3 destination;
    public void EnterState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;

        customer.table = CustomerManager.instance.SetTable();
        destination = customer.table.chair.position;
        customer.agent.SetDestination(destination);
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;

        float dist = customer.agent.remainingDistance;
        if (dist!=Mathf.Infinity && customer.agent.pathStatus == NavMeshPathStatus.PathComplete && customer.agent.remainingDistance == 0)
        { 
            stateManager.SwitchState(customer, customer.eat);
        }
    }
}
