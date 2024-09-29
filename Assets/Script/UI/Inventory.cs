using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
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
            playerInput.Player.Enable();
        } else {
            inventoryUI.SetActive(true);
            playerInput.UI.Enable();
            playerInput.Player.Disable();
        }
    }

    private void Start() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Inventory.performed += OpenInventory;

       for (int i = 0; i < cellArray.GetLength(0); i++)
       {
            for (int j = 0; j < cellArray.GetLength(1); j++)
            {
                Cell instantiatedCell = Instantiate(cell, canvas);
                instantiatedCell.Coordinate(i,j);
                cellArray[i,j] = instantiatedCell;   
            }
       }

       foreach (var item in cellArray)
        {
            item.Setup(cellArray);
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
            selectedGrid.AddItem(item, count);
            listItem.Add(item);
        } else {
            Cell selectedGrid = cellArray.Cast<Cell>().First(s => s.item == item);
            selectedGrid.AddItem(item, count);
        }
    }
    public void RemoveItem(DropItem item, int count) {
        if (listItem.Contains(item))
        {
            Cell selectedGrid = cellArray.Cast<Cell>().First(s => s.item == item);
            selectedGrid.RemoveItem(count);
            if (selectedGrid.item == null)
            {
                listItem.Remove(item);
            }
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
}
