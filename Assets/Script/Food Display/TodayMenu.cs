using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TodayMenu : MonoBehaviour
{
    [SerializeField] private List<MenuList> menuList;
    private List<Food> listFoods = new List<Food>();

    public void GenerateList(Food item) {
        if (!listFoods.Contains(item))
        {
            MenuList instantiatedMenuList = menuList.First(s => s.food == null);
            instantiatedMenuList.Setup(item);
            listFoods.Add(item);
        } else {
            MenuList instantiatedMenuList = menuList.First(s => s.food == item);
            instantiatedMenuList.AddFood(item);
        }
    }

    public void RemoveFromList(Food item) {
        if (listFoods.Contains(item))
        {
            MenuList instantiatedMenuList = menuList.First(s => s.food == item);
            instantiatedMenuList.RemoveFood();
            listFoods.Remove(item);
        }
    }

    public List<Food> GetFoodsOnSale() {
        return listFoods;
    }
}
