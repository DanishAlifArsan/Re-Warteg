using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionTab : SelectionBaseClass
{
    protected override void OnEnable() {
        base.OnEnable();
        playerInput.UI.NextPage.performed += NextTab;
        playerInput.UI.PrevPage.performed += PrevTab;
    }

    protected override void OnDisable() {
        base.OnDisable();
        playerInput.UI.NextPage.performed -= NextTab;
        playerInput.UI.PrevPage.performed -= PrevTab;
    }
    
    private void StartNavigate() {
        uiSelections[currentIndex].OnDeselect();
        OnDeselectEvent?.Invoke(uiSelections[currentIndex]);
    }
    private void EndNavigate() {
        uiSelections[currentIndex].OnSelect();
        OnSelectEvent?.Invoke(uiSelections[currentIndex]);
    }

    private void NextTab(InputAction.CallbackContext context)
    {
        StartNavigate();
        currentIndex++;
        if (currentIndex > uiSelections.Count - 1) {
            currentIndex = 0;
        }
        EndNavigate();
    }

    private void PrevTab(InputAction.CallbackContext context)
    {
        StartNavigate();
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = uiSelections.Count - 1;
        }
        EndNavigate();
    }
}
