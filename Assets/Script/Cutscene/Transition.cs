using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Action OnEndTransition;
    public void EndTransition() {
        OnEndTransition?.Invoke();
    }

    private void OnDestroy() {
        OnEndTransition += null;
    }
}
