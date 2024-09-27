using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodList : UISelection
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI durationText;
    [SerializeField] private Image recipeImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject activeFoodGameobject;
    public Food food;
    public bool isEmpty = true;
    public bool isFinished = false;
    public float remainingTime;

    public void Setup(Food _food) {
        food = _food;
        nameText.text = food.foodName;
        recipeImage.sprite = food.foodImage;
        remainingTime = food.cookTime;
        durationText.text = food.cookTime + "s";
        activeFoodGameobject.SetActive(true);
        image = backgroundImage;
        text = nameText;
        isEmpty = false;
    }   

    public void EndCook() {
        // food = null;     // pindah ketika makanannya diambil
        // isEmpty = true;
        // activeFoodGameobject.SetActive(false); 
        isFinished = true;
        durationText.text = "finished";
    }


    public void StartCooking() {
        remainingTime -= Time.deltaTime;
        durationText.text = ((int) remainingTime) + "s";
        if (remainingTime <= 0)
        {
            EndCook();
        } 
    }
}
