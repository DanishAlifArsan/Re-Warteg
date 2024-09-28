using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TodayMenu menuUI;
    public List<Food> listFoodOnSale = new List<Food>(); 
    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void GenerateList(Food item) {
        menuUI.GenerateList(item);
        listFoodOnSale = menuUI.GetFoodsOnSale();
    }   

    public void RemoveFromList(Food food) {
        menuUI.RemoveFromList(food);
        listFoodOnSale = menuUI.GetFoodsOnSale();
    }
}
