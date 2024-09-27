using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionBaseClass : MonoBehaviour
{
    public List<UISelection> uiSelections = new List<UISelection>();
    public UnityEvent<UISelection> OnSelectEvent;
    public UnityEvent<UISelection> OnDeselectEvent;
    protected PlayerInput playerInput;
    protected int currentIndex;
    
    protected virtual void OnEnable() {
        currentIndex = 0;
        playerInput = InputManager.instance.playerInput;
        playerInput.UI.Apply.performed += Apply;  
        uiSelections[currentIndex].OnSelect();
        OnSelectEvent?.Invoke(uiSelections[currentIndex]);
    }

    protected virtual void Apply(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        uiSelections[currentIndex].OnConfirm();
    }

    protected virtual void OnDisable() {
        playerInput.UI.Apply.performed -= Apply; 
        uiSelections[currentIndex].OnDeselect();
        OnDeselectEvent?.Invoke(uiSelections[currentIndex]);
    }
}
