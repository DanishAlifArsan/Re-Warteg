using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFood : MonoBehaviour
{
    [SerializeField] List<Recipe> RecipeList;
    PlayerInput playerInput;
    int currentIndex = 0;

    private void OnEnable() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Mouse.Disable();
        playerInput.Player.Disable();
        playerInput.UI.Enable();
        playerInput.UI.Navigate.performed += Navigate;
        playerInput.UI.Apply.performed += ConfirmFood;
        playerInput.UI.Cancel.performed += Cancel;
        RecipeList[currentIndex].Select();
    }

    private void Cancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        RecipeList[currentIndex].Deselect();
        gameObject.SetActive(false);
    }

    private void ConfirmFood(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        RecipeList[currentIndex].Confirm();
    }

    private void OnDisable() {
        playerInput.Mouse.Enable();
        playerInput.Player.Enable();
        playerInput.UI.Disable();
    }

    private void Navigate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 pos = playerInput.UI.Navigate.ReadValue<Vector2>();
        RecipeList[currentIndex].Deselect();
        currentIndex += (int) pos.y * -1;

        if (currentIndex < 0) {
            currentIndex = RecipeList.Count - 1;
        } else if (currentIndex > RecipeList.Count - 1) {
            currentIndex = 0;
        }

        RecipeList[currentIndex].Select();
    }
}
