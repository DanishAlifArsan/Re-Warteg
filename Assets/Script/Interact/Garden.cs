using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour, Interactable
{
    [SerializeField] private List<DropItem> item;
    public string FlavorText()
    {
       return "Pick";
    }

    public void OnInteract()
    {
        gameObject.SetActive(false);
        int randItem = Random.Range(0, item.Count);
        int randAmount = Random.Range(5, 11);

        Inventory.instance.AddItem(item[randItem], randAmount);
        GardenManager.instance.Pickup(item[randItem], randAmount);
        TimeManager.instance.CountDungeonTime(30);
    }
}
