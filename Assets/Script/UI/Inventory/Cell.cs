using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : UISelection
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCountText;
    public DropItem item;
    private int itemCount = 0;
    public void AddItem(DropItem _item, int count) {
        if (item == null)
        {
            item = _item;
            itemImage.color = Color.white;
            itemImage.sprite = item.itemImage;
        }
        itemCount += count;
        itemCountText.text = itemCount.ToString();
    }

    public void RemoveItem(int count) {
        itemCount -= count;
        itemCountText.text = itemCount.ToString();
        if (itemCount <= 0)
        {
            itemCount = 0;
            item = null;
            InActive();
        }
    }

    public bool CheckItem(int count) {
        return count <= itemCount;
    }

    public int CheckCount(DropItem _item) {
        if (item == _item)
        {
            return itemCount;
        } else {
            return 0;
        }
    }

    private void InActive() {
        itemImage.color = new Color(0, 0, 0, 0);
        itemImage.sprite = null;
        itemCountText.text = "";
    }
}
