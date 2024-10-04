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

    public bool BuyCondition() {
        if (listFoodOnSale.Count < 0)
        {
            return false;
        } else {
            return listFoodOnSale.Any(s => s.foodType == FoodType.Rice) && listFoodOnSale.Count > 1;
        }
    }

    public Food GetRice() {
        return listFoodOnSale.First(s => s.foodType == FoodType.Rice);
    }

    public List<Food> GetLauk() {
        return listFoodOnSale.Where(s => s.foodType == FoodType.Lauk).ToList();
    }

    public bool CompareItem(List<Food> foodOnPlate) {
        List<Food> foodToBuy = CustomerManager.instance.currentCustomer.foodToBuy;
        return foodOnPlate.Count == foodToBuy.Count && !foodOnPlate.Except(foodToBuy).Any();
    }
}
