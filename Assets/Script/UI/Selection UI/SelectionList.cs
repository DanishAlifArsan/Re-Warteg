using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionList : SelectionBaseClass
{   
    protected override void OnEnable() {
        base.OnEnable();
        playerInput.UI.Navigate.performed += Navigate;
    }

    protected virtual void Navigate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 pos = playerInput.UI.Navigate.ReadValue<Vector2>();
        uiSelections[currentIndex].OnDeselect();
        OnDeselectEvent?.Invoke(uiSelections[currentIndex]);
        currentIndex += (int) pos.y * -1;

        if (currentIndex < 0) {
            currentIndex = uiSelections.Count - 1;
        } else if (currentIndex > uiSelections.Count - 1) {
            currentIndex = 0;
        }

        uiSelections[currentIndex].OnSelect();
        OnSelectEvent?.Invoke(uiSelections[currentIndex]);
    }

    protected override void OnDisable() {
        base.OnDisable();
        playerInput.UI.Navigate.performed -= Navigate;
    }
}