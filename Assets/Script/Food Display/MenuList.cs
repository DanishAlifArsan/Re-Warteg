using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuList : MonoBehaviour
{
    [SerializeField] private Image foodImage;
    [SerializeField] private TextMeshProUGUI remainingCountText;
    [SerializeField] private TextMeshProUGUI totalCountText;
    public Food food;
    private int remainingFood;
    
    public void Setup(Food item) {    
        food = item;
        foodImage.color = Color.white;
        foodImage.sprite = item.foodImage;
        remainingFood = item.porsi;
        remainingCountText.text = remainingFood.ToString();
        totalCountText.text = item.porsi.ToString();
    }

    public void AddFood(Food item) {
        remainingFood += item.porsi;
        remainingCountText.text = remainingFood.ToString();
        if (remainingFood > item.porsi)
        {
            totalCountText.text = remainingFood.ToString();
        }
    }

    public void RemoveFood(List<Food> listFoods) {
        remainingFood -= 1;
        remainingCountText.text = remainingFood.ToString();
        if (remainingFood <= 0)
        {
            InActive();
            food = null;
            listFoods.Remove(food);
        }
    }

    private void InActive() {
        foodImage.color = new Color(1,1,1,0);
        remainingCountText.text = "";
        totalCountText.text = "";
    }
}
