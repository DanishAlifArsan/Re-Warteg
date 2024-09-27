using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/DropItem")]
public class DropItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int price;
}
