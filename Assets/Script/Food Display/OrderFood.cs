using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderFood : MonoBehaviour
{
    [SerializeField] private Image foodImage;

    public void Setup(Food food) {
        foodImage.color = Color.white;
        foodImage.sprite = food.foodImage;
    }

    public void InActive() {
        foodImage.color = new Color(1, 1, 1, 0);
    }
}
