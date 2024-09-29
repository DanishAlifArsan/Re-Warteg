using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Material : MonoBehaviour
{
    [SerializeField] private Image materialImage;
    [SerializeField] private TextMeshProUGUI materialCount;

    public void Setup(DropItem item, int count) {
        materialImage.color = Color.white;
        materialImage.sprite = item.itemImage;
        int itemCount = Inventory.instance.GetItemCount(item);
        materialCount.text = itemCount+"/"+ count;
    }

    public void InActive() {
        materialImage.color = new Color(1,1,1,0);
        materialCount.text = "";
    }
}
