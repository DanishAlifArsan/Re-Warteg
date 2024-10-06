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
    private int posX, posY;

    public void Coordinate(int posX, int posY)
    {
        this.posX = posX;
        this.posY = posY;
    }

    public void AddItem(DropItem _item, int count) {
        if (item == null)
        {
            item = _item;
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
        itemImage.sprite = null;
        itemCountText.text = "";
    }
}
