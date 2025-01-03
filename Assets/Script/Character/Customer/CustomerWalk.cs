using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerWalk : IState
{
    private Vector3 destination;
    public void EnterState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;
        customer.anim.SetBool("walk",true);
        // customer.isWalking = true;
        // customer.dialogueBubbleUI.SetActive(false);
        if (customer.isBuying)
        {
            destination = customer.homePoint.position;
        } else {
            destination = customer.cashierPoint.position;
        }
        customer.agent.SetDestination(destination);
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;

       float dist = customer.agent.remainingDistance;
       if (dist!=Mathf.Infinity && customer.agent.pathStatus == NavMeshPathStatus.PathComplete && customer.agent.remainingDistance == 0)
       {
            // customer.isWalking = false;
            if (customer.isBuying)
            {
                CustomerManager.instance.DespawnCustomer(customer);
            } else {
                stateManager.SwitchState(customer, customer.buy);
            }
       }
    }
}
