using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionButton : UISelection
{
    [SerializeField] private UnityEvent onClickEvent;
    public GameObject indicator;

    public override void OnConfirm()
    {
        onClickEvent.Invoke();
    }
}
