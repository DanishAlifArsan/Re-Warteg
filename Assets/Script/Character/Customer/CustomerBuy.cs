using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerBuy : IState
{
    public void EnterState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;
        AudioManager.instance.PlaySound(customer.buySound);
        // customer.anim.SetTrigger("buy");
        customer.SetFoodsToBuy();
        customer.buyIndicator.SetActive(true);
        MenuManager.instance.GenerateOrder(customer.foodToBuy);  // setup ui di display makanan
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        CustomerAI customer = user as CustomerAI;
        
        customer.isBuying = true;
        if (customer.isGetFood)     // to do atur isgetfood dan assign piring ke customer. buat piring jadi not interactable
        {
            // customer.speak.Happy();
            CustomerManager.instance.customerAmount += 1;

            customer.buyIndicator.SetActive(false);
            stateManager.SwitchState(customer, customer.food);
        }
    }
}
