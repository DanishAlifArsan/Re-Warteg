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
    [SerializeField] private GameObject inactiveFoodGameobject;
    [SerializeField] private Color32 activeSelectedImageColor;
    public Food food;
    public bool isEmpty = true;
    public bool isFinished = false;
    public float remainingTime;
    private Image inactiveImage;
    private TextMeshProUGUI inactiveText;
    private Color32 inactiveSelectedImageColor;

    public void Setup(Food _food) {
        food = _food;
        nameText.text = food.foodName;
        recipeImage.sprite = food.foodImage;
        remainingTime = food.cookTime;
        durationText.text = food.cookTime + "s";
        inactiveFoodGameobject.SetActive(false);
        activeFoodGameobject.SetActive(true);

        inactiveImage = image;
        inactiveText = text;
        inactiveSelectedImageColor = selectedImageColor;

        image = backgroundImage;
        text = nameText;
        selectedImageColor = activeSelectedImageColor;
        isEmpty = false;
    }   

    public void EndCook() {
        isFinished = true;
        durationText.text = "finished";
    }

    public void FinishCook() {
        food = null;     // pindah ketika makanannya diambil
        isEmpty = true;
        isFinished = false;
        activeFoodGameobject.SetActive(false); 
        inactiveFoodGameobject.SetActive(true);

        image = inactiveImage;
        text = inactiveText;
        selectedImageColor = inactiveSelectedImageColor;
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
