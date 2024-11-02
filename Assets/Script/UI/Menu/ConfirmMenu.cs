using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmMenu : MonoBehaviour
{
    public Action OnConfirm;
    private PlayerInput playerInput;
    private void OnEnable() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Disable();
        playerInput.Kitchen.Disable();
        playerInput.Dungeon.Disable();
        playerInput.UI.Enable();
        Time.timeScale = 0;
        playerInput.UI.Apply.performed += Apply;
        playerInput.UI.Cancel.performed += Cancel;
    }

    private void Apply(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnConfirm?.Invoke();
    }

    protected virtual void Cancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        playerInput.Player.Enable();
        playerInput.Kitchen.Enable();
        playerInput.Dungeon.Enable();
        playerInput.UI.Disable();
        Time.timeScale = 1;
        playerInput.UI.Apply.performed -= Apply;
        playerInput.UI.Cancel.performed -= Cancel;
    }
}
