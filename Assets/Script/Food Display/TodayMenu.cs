using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TodayMenu : MonoBehaviour
{
    [SerializeField] private List<MenuList> menuList;
    private List<Food> listFoods = new List<Food>();

    public void GenerateList(Food item) {
        if (!listFoods.Contains(item))
        {
            MenuList instantiatedMenuList = menuList.First(s => s.food == null);    // ubah supaya compare berdasarkan nama item
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
            if (instantiatedMenuList.food == null)
            {
                item.menuDisplay.Inactive();
                item.menuDisplay = null;
                listFoods.Remove(item); // ubah supaya hanya kehapus ketika porsi udah habis
            }
        }
    }

    public List<Food> GetFoodsOnSale() {
        return listFoods;
    }
}
