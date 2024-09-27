using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookFood : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeDuration;
    [SerializeField] Image recipeImage;
    [SerializeField] List<Image> materialImage;
    [SerializeField] List<TextMeshProUGUI> materialCount;
    
    public void SelectRecipe(UISelection uISelection) {
        Recipe recipe = uISelection.GetComponent<Recipe>();
        recipeImage.sprite = recipe.food.foodImage;
        recipeDuration.text = "duration: "+ recipe.food.cookTime +"s";
        materialImage[0].sprite = recipe.food.materialsItem[0].itemImage;
        materialCount[0].text = recipe.food.materialsCount[0].ToString();
        materialImage[1].sprite = null;
    }

    public void DeselectRecipe(UISelection uISelection) {
        recipeImage.sprite = null;
        recipeDuration.text ="";
    }

    public void SelectTab(UISelection uISelection) {
        uISelection.GetComponent<Tab>().ShowMenu(true);
    }

    public void DeselectTab(UISelection uISelection) {
        uISelection.GetComponent<Tab>().ShowMenu(false);
    }

    private PlayerInput playerInput;
    
    private void OnEnable() {
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
