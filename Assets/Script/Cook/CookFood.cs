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
    [SerializeField] List<Material> materialList;
    
    public void SelectRecipe(UISelection uISelection) {
        Recipe recipe = uISelection.GetComponent<Recipe>();
        recipeImage.color = Color.white;
        recipeImage.sprite = recipe.food.foodImage;
        recipeDuration.text = "duration"+recipe.food.cookTime+"s";
        recipe.SetupMaterial(materialList);
    }

    public void DeselectRecipe(UISelection uISelection) {
        recipeImage.color = new Color(1,1,1,0);
        recipeDuration.text ="";

        for (int i = 0; i < materialList.Count  ; i++)
        {
            materialList[i].InActive();
        }
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
        playerInput.Kitchen.Disable();
        playerInput.UI.Enable();
        playerInput.UI.Cancel.performed += Cancel;
    }

    protected virtual void Cancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        TutorialManager.instance?.NextTutorial(2);
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        playerInput.Player.Enable();
        playerInput.Kitchen.Enable();
        playerInput.UI.Disable();
    }
}
