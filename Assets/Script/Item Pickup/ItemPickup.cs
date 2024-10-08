using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;
    
    public void Pickup(DropItem item, int count) {
        itemName.text = item.itemName;
        itemImage.sprite = item.itemImage;
        itemCount.text = "+"+count;
    }
}
