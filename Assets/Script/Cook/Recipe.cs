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

    public void Setup(Food _food) {
        food = _food;
        nameText.text = food.foodName;
        recipeImage.sprite = food.foodImage;
        durationText.text = food.cookTime + "s";
    }   

    public override void OnConfirm()
    {
        QueueFood.instance.AddToQueue(food);
        TutorialManager.instance?.NextTutorial(1);
    }
}
