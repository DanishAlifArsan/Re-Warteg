using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/Food")]
public class Food : ScriptableObject, HoldItem
{
    public string foodName;
    public int porsi;
    public Sprite foodImage;
    public float cookTime;
    public int price;
    public GameObject prefab;
    public List<DropItem> materialsItem;
    public List<int> materialsCount;
    public ItemType type;

    public ItemType itemType { get => type; set => itemType = type; }
    public MenuDisplay menuDisplay;
}
