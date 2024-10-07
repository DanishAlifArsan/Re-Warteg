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
        playerInput.Kitchen.Disable();
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

        if (CustomerManager.instance.currentCustomer == null)
        {
            return;
        }

        if (MenuManager.instance.CompareItem(plate.GetFood()) ) // cek apakah makanan di piring sama dengan makanan yang diinginkan pelanggan
        {
            MenuManager.instance.RemoveOrder();

            PlateManager.instance.TakeFood(plate);
            CustomerManager.instance.currentCustomer.isGetFood = true;
            gameObject.SetActive(false);
            display.SetupCamera(false);
        }
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
        } else {
            Debug.Log("No Food");  // ganti jadi warning
        }
    }

    private void OnDisable() {
        playerInput.Player.Enable();
        playerInput.Kitchen.Enable();
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
        // MenuManager.instance.RemoveOrder();
    }
}
