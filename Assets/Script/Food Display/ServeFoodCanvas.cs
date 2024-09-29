using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServeFoodCanvas : UISelection
{
    [SerializeField] private TextMeshProUGUI nameText;
    public Food food;
    private MenuDisplay menuDisplay;

    public void Setup(Food _food) {
        food = _food;
        nameText.text = food.foodName;
    }   

    public void Inactive() {
        food = null;
        nameText.text = "";
    }
}
