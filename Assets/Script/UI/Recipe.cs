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
    public Food food;
    QueueFood queueFood;

    public void Setup(Food _food, QueueFood _queueFood) {
        food = _food;
        nameText.text = food.foodName;
        recipeImage.sprite = food.foodImage;
        durationText.text = food.cookTime + "s";

        queueFood = _queueFood;
    }   

    public override void OnConfirm()
    {
        queueFood.AddToQueue(food);
    }
}
