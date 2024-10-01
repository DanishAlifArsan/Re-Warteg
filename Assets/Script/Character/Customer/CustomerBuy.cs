using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerBuy : IState
{
    public bool isRunning;
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.anim.SetTrigger("buy");
        isRunning = false;
        customer.SetGoodsToBuy();
        customer.dialogueBubbleUI.SetActive(true);
        CustomerManager.instance.currentCustomer = customer;
        SaleManager.instance.SetupTable(customer.goodsToBuy.Values.Max(), customer.goodsToBuy.Count);
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        customer.isBuying = true;
        if (customer.isPaying)
        {
            customer.ClearGoodsToBuy();
            customer.speak.Happy();
            stateManager.SwitchState(customer, stateManager.pay);
        } else if(customer.isWalking) {   
            customer.ClearGoodsToBuy(); 
            // kalau endless run jadi
            // if (!SaleManager.instance.CheckIsTableEmpty() && !isRunning)
            // {
            //     EndlessRunManager.instance.StartRunning(false);
            //     EndlessRunManager.instance.chasedCustomer = customer;
            //     isRunning = true;
            // } else {
            //     SaleManager.instance.EmptyTable();
            // }
            //
            //kalau endess run gak jadi
            SaleManager.instance.EmptyTable();
            // 
            customer.speak.Angry();
            stateManager.SwitchState(customer, stateManager.walk);
            CustomerManager.instance.currentCustomer = null;
        }
    }
}
