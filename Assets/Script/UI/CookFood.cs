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
    // private bool isRecipeShown;

    public void SelectRecipe(UISelection uISelection) {
        Recipe recipe = uISelection.GetComponent<Recipe>();
        recipeText.text = recipe.GetName();
    }

    public void DeselectRecipe(UISelection uISelection) {
        recipeText.text = "";
    }

    public void SelectTab(UISelection uISelection) {
        // Debug.Log(isRecipeShown);
        // if (isRecipeShown)
        // {
        //     cookObject.SetActive(false);
        //     queueObject.SetActive(true);
        //     isRecipeShown = false;
        // } else {
        //     cookObject.SetActive(true);
        //     queueObject.SetActive(false);
        //     isRecipeShown = true;
        // }
        uISelection.GetComponent<Tab>().ShowMenu(true);
    }

    public void DeselectTab(UISelection uISelection) {
        uISelection.GetComponent<Tab>().ShowMenu(false);
    }

    private PlayerInput playerInput;
    
    private void OnEnable() {
        // isRecipeShown = true;
        // Debug.Log("onenable" + isRecipeShown);
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Disable();
        playerInput.UI.Enable();
        playerInput.UI.Cancel.performed += Cancel;
    }

    protected virtual void Cancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        playerInput.Player.Enable();
        playerInput.UI.Disable();
    }
}
