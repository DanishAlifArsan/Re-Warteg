using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI menuNameText;
    [SerializeField] private Transform foodPos;
    public Food food;
    private GameObject instantiatedFood;
    
    public void Setup(Food _food) {
        food = _food;
        instantiatedFood = Instantiate(food.prefab, foodPos);
        menuNameText.text = food.foodName;
    }

    public void Inactive() {
        Destroy(instantiatedFood);
    }
}
