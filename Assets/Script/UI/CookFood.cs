using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookFood : MonoBehaviour
{
    [SerializeField] SelectionList selectionList;
    [SerializeField] TextMeshProUGUI recipeText;
    [SerializeField] GameObject cookObject;
    [SerializeField] GameObject queueObject;
    private bool isRecipeShown;

    public void SelectRecipe(UISelection uISelection) {
        Recipe recipe = uISelection.GetComponent<Recipe>();
        recipeText.text = recipe.GetName();
    }

    public void DeselectRecipe(UISelection uISelection) {
        recipeText.text = "";
    }

    public void ChangeTab(UISelection uISelection) {
        if (isRecipeShown)
        {
            cookObject.SetActive(false);
            queueObject.SetActive(true);
            isRecipeShown = false;
        } else {
            cookObject.SetActive(true);
            queueObject.SetActive(false);
            isRecipeShown = true;
        }
    }

    private PlayerInput playerInput;
    
    private void OnEnable() {
        isRecipeShown = false;
        playerInput = InputManager.instance.playerInput;
        playerInput.Mouse.Disable();
        playerInput.Player.Disable();
        playerInput.UI.Enable();
        playerInput.UI.Cancel.performed += Cancel;
    }

    protected virtual void Cancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        playerInput.Mouse.Enable();
        playerInput.Player.Enable();
        playerInput.UI.Disable();
    }
}
