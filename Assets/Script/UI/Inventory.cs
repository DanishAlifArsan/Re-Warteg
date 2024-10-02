using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<DropItem> startingItem;
    [SerializeField] List<int> startingItemCount;
    [SerializeField] private Cell cell;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemdescText;
    [SerializeField] private GameObject inventoryUI;
    private List<DropItem> listItem = new List<DropItem>();
    public Cell[,] cellArray = new Cell[5,4]; 
    public static Inventory instance;
    PlayerInput playerInput;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }


    private void OpenInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (inventoryUI.activeInHierarchy)
        {   
            inventoryUI.SetActive(false);
            playerInput.UI.Disable();
        } else {
            inventoryUI.SetActive(true);
            playerInput.UI.Enable();
        }
    }

    private void Start() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Inventory.performed += OpenInventory;

       for (int i = 0; i < cellArray.GetLength(1); i++)
       {
            for (int j = 0; j < cellArray.GetLength(0); j++)
            {
                Cell instantiatedCell = Instantiate(cell, canvas);
                instantiatedCell.Coordinate(j,i);
                cellArray[j,i] = instantiatedCell;   
            }
       }

       for (int i = 0; i < Math.Min(startingItem.Count, startingItemCount.Count); i++)
       {
            AddItem(startingItem[i], startingItemCount[i]);
       }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(DropItem item, int count) {
        if (!listItem.Contains(item))
        {
            Cell selectedGrid = cellArray.Cast<Cell>().First(s => s.item == null); 
            // Cell selectedGrid = Search(null); 
            selectedGrid.AddItem(item, count);
            listItem.Add(item);
        } else {
            // Cell selectedGrid = cellArray.Cast<Cell>().First(s => s.item == item);
            Cell selectedGrid = Search(item.itemName);
            selectedGrid.AddItem(item, count);
        }
    }
    public void RemoveItem(DropItem item, int count) {
        if (listItem.Contains(item))
        {
            // Cell selectedGrid = cellArray.Cast<Cell>().First(s => s.item == item);
            Cell selectedGrid = Search(item.itemName);
            selectedGrid.RemoveItem(count);
            if (selectedGrid.item == null)
            {
                listItem.Remove(item);
            }
        }
    }

    public bool CheckItem(List<DropItem> item, List<int> count) {
        bool status = false;
        for (int i = 0; i < Math.Min(item.Count, count.Count); i++)
        {
            if (listItem.Contains(item[i]))
            {
                // status = cellArray.Cast<Cell>().First(s => s.item.itemName == item[i].itemName).CheckItem(count[i]);
                status = Search(item[i].itemName).CheckItem(count[i]);
            } else {
                status = false;
                break;
            }
        }
        return status;
    }

    public int GetItemCount(DropItem item) {
        if (listItem.Contains(item))
        {
            // return cellArray.Cast<Cell>().First(s => s.item.itemName == item.itemName).CheckCount(item);
            return Search(item.itemName).CheckCount(item);;
        } else {
            return 0;
        }
    }

    public void selectItem(UISelection uISelection) {
        Cell cell = uISelection as Cell;
        if (cell.item != null)
        {
            itemImage.color = Color.white;
            itemImage.sprite = cell.item.itemImage;
            itemNameText.text = cell.item.itemName;
            itemdescText.text = cell.item.desc;
        } else {
            itemImage.color = new Color(1,1,1,0);
            itemNameText.text = "";
            itemdescText.text = "";
        }
    }

    public void DeselectItem(UISelection uISelection) {
        itemImage.color = new Color(1,1,1,0);
        itemNameText.text = "";
        itemdescText.text = "";
    }

    private Cell Search(string name) {
        for (int i = 0; i < cellArray.GetLength(0); i++) {
            for (int j = 0; j < cellArray.GetLength(1); j++) {
                if (cellArray[i, j].item != null)
                {
                    if (cellArray[i, j].item.name == name) {
                        return cellArray[i, j];
                    }
                }
            }
        }
        return null;
    }
}