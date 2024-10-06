using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionButton : UISelection
{
    [SerializeField] private UnityEvent onClickEvent;
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnConfirm);
    }

    public override void OnConfirm()
    {
        onClickEvent.Invoke();
    }
}
