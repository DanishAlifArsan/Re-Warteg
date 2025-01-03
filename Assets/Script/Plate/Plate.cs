using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour, Interactable, HoldItem
{
    [SerializeField] private PlayerInteract player;
    [SerializeField] private ItemType type = ItemType.Plate;
    [SerializeField] private Transform foodPos;
    public ItemType itemType { get => type; set => itemType = type; }
    public Table table;
    private Dictionary<Food, GameObject> foodList = new Dictionary<Food,GameObject>();

    public string FlavorText()
    {
        if (player.itemInHand == null) {
            return "Take";
        } else {
            return "";
        }
    }

    public void OnInteract()
    {
        if (player.itemInHand == null)
        {
            // gameObject.SetActive(false);      // ganti dengan pindah piring ke player
            // isAbleToInteract = false;
            gameObject.layer = 0;
            transform.parent = player.platePos;
            transform.localPosition = Vector3.zero;
            table.isOccupied = false;
            PlateManager.instance.TakePlate(this);
            player.itemInHand = this;
        }
    }

    public void AddFood(Food food) {   
        if (!foodList.ContainsKey(food))
        {
            GameObject instantiatedFood = Instantiate(food.prefab, foodPos.position, foodPos.rotation, foodPos);
            foodList.Add(food, instantiatedFood);
        }
    }

    public void RemoveFood(Food food) {
        if (foodList.ContainsKey(food))
        {
            Destroy(foodList[food]);
            foodList.Remove(food);
        }
    }

    public void ConfirmFood() {
        foreach (var item in foodList)
        {
            MenuManager.instance.RemoveFromList(item.Key);
        }
    }

    public void EmptyFood() {
        foreach (var item in foodList)
        {
            Destroy(item.Value);
        }
        foodList.Clear();
    }

    public bool CheckIsEmpty() {
        return foodList.Count <= 0;
    }

    public List<Food> GetFood() {
        return new List<Food>(foodList.Keys);
    }
}
