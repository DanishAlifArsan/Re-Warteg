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
        // customer.dialogueBubbleUI.SetActive(true);
        CustomerManager.instance.currentCustomer = customer;
        MenuManager.instance.GenerateOrder(customer.foodToBuy);  // setup ui di display makanan
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        customer.isBuying = true;
        if (customer.isGetFood)     // to do atur isgetfood dan assign piring ke customer. buat piring jadi not interactable
        {
            customer.ClearFoodsToBuy();
            // customer.speak.Happy();
            stateManager.SwitchState(customer, stateManager.food);
            CustomerManager.instance.currentCustomer = null;
        }
    }
}
