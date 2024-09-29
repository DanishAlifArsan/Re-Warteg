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
    public Cell up, down, left, right;
    public DropItem item;
    private int itemCount = 0;
    private int posX, posY;

    public void Coordinate(int posX, int posY)
    {
        this.posX = posX;
        this.posY = posY;
    }

    public void Setup(Cell[,] cellArray) {
        int x = cellArray.GetLength(0) - 1;
        int y = cellArray.GetLength(1) - 1;

        up =  posY <= 0 ? null : cellArray[posX, posY - 1];
        down = posY == y ? null : cellArray[posX, posY + 1];
        left = posX <= 0 ? null : cellArray[posX - 1 , posY];
        right = posX == x? null : cellArray[posX + 1, posY];
    }

    public void AddItem(DropItem _item, int count) {
        if (item == null)
        {
            item = _item;
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

    private void InActive() {
        itemCountText.text = "";
    }
}
