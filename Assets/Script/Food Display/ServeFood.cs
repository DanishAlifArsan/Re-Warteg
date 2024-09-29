using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeFood : MonoBehaviour
{
    private PlayerInput playerInput;
    private Display display;
    private Plate plate;
    ServeFoodCanvas selectedCanvas;
    
    private void OnEnable() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Disable();
        playerInput.UI.Enable();
        playerInput.UI.Cancel.performed += CancelInteract;
        playerInput.UI.Action1.performed += AddFood;
        playerInput.UI.Action2.performed += RemoveFood;
        playerInput.UI.Apply.performed += ConfirmFood;
    }

    private void ConfirmFood(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (plate.CheckIsEmpty())
        {
            Debug.Log("Please select food");  // ganti jadi warning
            return;
        }

        PlateManager.instance.TakeFood(plate);
        gameObject.SetActive(false);
        display.SetupCamera(false);
    }

    private void RemoveFood(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (selectedCanvas.food != null)
        {
            plate.RemoveFood(selectedCanvas.food);
        }
    }

    private void AddFood(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (selectedCanvas.food != null)
        {
            plate.AddFood(selectedCanvas.food);
        }
    }

    private void OnDisable() {
        playerInput.Player.Enable();
        playerInput.UI.Disable();
        playerInput.UI.Cancel.performed -= CancelInteract;
        playerInput.UI.Action1.performed -= AddFood;
        playerInput.UI.Action2.performed -= RemoveFood;
        playerInput.UI.Apply.performed -= ConfirmFood;
    }

    public void Setup(Display _display, Plate _plate) {
        gameObject.SetActive(true);
        display = _display;
        plate = _plate;
    }

    public void SelectRecipe(UISelection uISelection) {
        ServeFoodCanvas serveFoodCanvas = uISelection as ServeFoodCanvas;
        selectedCanvas = serveFoodCanvas;
    }

    private void CancelInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
        display.SetupCamera(false);
        PlateManager.instance.CancelPrepare(plate);
    }
}