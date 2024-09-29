using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : UISelection
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI durationText;
    [SerializeField] private Image recipeImage;
    private List<Material> materials = new List<Material>();
    public Food food;

    public void Setup(Food _food) {
        food = _food;
        nameText.text = food.foodName;
        recipeImage.sprite = food.foodImage;
        durationText.text = food.cookTime + "s";
    }   

    public void SetupMaterial(List<Material> materialList) {
        materials = materialList;
        for (int i = 0; i < Mathf.Min(food.materialsItem.Count,materialList.Count)  ; i++)
        {
            materials[i].Setup(food.materialsItem[i],food.materialsCount[i]);
        }
    }

    public override void OnConfirm()
    {
        if (Inventory.instance.CheckItem(food.materialsItem, food.materialsCount))
        {
            for (int i = 0; i < Math.Min(food.materialsItem.Count, food.materialsCount.Count); i++)
            {
                Inventory.instance.RemoveItem(food.materialsItem[i], food.materialsCount[i]);
            }
            SetupMaterial(materials);
            QueueFood.instance.AddToQueue(food);
            TutorialManager.instance?.NextTutorial(1);
        } else {
            Debug.Log("Not enough material");
        }
    }
}
