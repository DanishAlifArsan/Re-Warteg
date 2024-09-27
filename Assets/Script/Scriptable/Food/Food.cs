using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/Food")]
public class Food : ScriptableObject
{
    public string foodName;
    public int porsi;
    public Sprite foodImage;
    public float cookTime;
    public int price;
    public GameObject prefab;
    public List<DropItem> materialsItem;
    public List<int> materialsCount;
}
