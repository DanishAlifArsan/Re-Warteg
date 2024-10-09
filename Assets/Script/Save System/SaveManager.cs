using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public int totalCurrency;
    public int day;
    public SerializableDictionary<DropItem, int> inventoryItem = new SerializableDictionary<DropItem, int>();
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public void NewGame() {
        SaveSystem.Delete();
    }

    public void SaveGame() {
        SaveSystem.Save(this);
    }

    public GameData LoadGame() {
        GameData data = SaveSystem.Load();
        if (data != null)
        {
            totalCurrency = data.totalCurrency;
            day = data.day;
            inventoryItem = data.inventoryItem;
        }   

        return data;
    }
}
