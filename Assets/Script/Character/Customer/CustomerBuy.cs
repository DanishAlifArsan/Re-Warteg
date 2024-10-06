using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerBuy : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        // customer.anim.SetTrigger("buy");
        customer.SetFoodsToBuy();
        customer.buyIndicator.SetActive(true);
        MenuManager.instance.GenerateOrder(customer.foodToBuy);  // setup ui di display makanan
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        customer.isBuying = true;
        if (customer.isGetFood)     // to do atur isgetfood dan assign piring ke customer. buat piring jadi not interactable
        {
            // customer.speak.Happy();
            customer.buyIndicator.SetActive(false);
            stateManager.SwitchState(customer, stateManager.food);
        }
    }
}
