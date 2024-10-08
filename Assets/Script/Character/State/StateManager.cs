using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public IState currentState;
    public void StartState(StateUser user, IState state) {
        currentState = state;
        currentState.EnterState(user, this);
    }

    public void SwitchState(StateUser user, IState state) {
        currentState = state;
        state.EnterState(user, this);
    }
}
