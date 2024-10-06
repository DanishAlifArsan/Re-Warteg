using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public IState currentState;
    public CustomerIdle idle = new CustomerIdle();
    public CustomerWalk walk = new CustomerWalk();
    public CustomerBuy buy = new CustomerBuy();
    public CustomerEat eat = new CustomerEat();
    public CustomerFood food = new CustomerFood();

    public void StartState(CustomerAI customer) {
        currentState = idle;
        currentState.EnterState(customer, this);
    }

    public void SwitchState(CustomerAI customer, IState state) {
        currentState = state;
        state.EnterState(customer, this);
        Debug.Log(currentState);
    }
}
