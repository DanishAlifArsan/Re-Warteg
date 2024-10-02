using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TodayMenu menuUI;
    [SerializeField] private List<OrderFood> orderFoods;
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

    public void GenerateOrder(List<Food> foodToOrder) {
        for (int i = 0; i < foodToOrder.Count; i++)
        {
            orderFoods[i].Setup(foodToOrder[i]);
        }
    }

    public void RemoveOrder() {
        foreach (var item in orderFoods)
        {
            item.InActive();
        }
    }

     public bool CompareItem(List<Food> foodOnPlate) {
        List<Food> foodToBuy = CustomerManager.instance.currentCustomer.foodToBuy;
        return foodOnPlate.Count == foodToBuy.Count && !foodOnPlate.Except(foodToBuy).Any();
    }
}
