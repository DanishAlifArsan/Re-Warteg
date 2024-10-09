using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCurrency;
    public int day;
    public SerializableDictionary<DropItem, int> inventoryItem = new SerializableDictionary<DropItem, int>();
    public GameData(SaveManager manager) {
        totalCurrency = manager.totalCurrency;
        day = manager.day;
        inventoryItem = manager.inventoryItem;
    }
}
