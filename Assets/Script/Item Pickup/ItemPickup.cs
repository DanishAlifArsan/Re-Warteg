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
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }
    
    public void Pickup(DropItem item, int count) {
        anim.SetTrigger("appear");
        itemName.text = item.itemName;
        itemImage.sprite = item.itemImage;
        itemCount.text = "+"+count;
    }
}
