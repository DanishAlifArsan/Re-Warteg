using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionGrid : SelectionBaseClass
{
    private int curX;
    private int curY;
    Cell[,] curCell;
    protected override void OnEnable() {
        curCell = Inventory.instance.cellArray;
        // base.OnEnable();
        curX = 0;
        curY = 0;

        playerInput = InputManager.instance.playerInput;
        curCell[curX,curY].OnSelect();
        OnSelectEvent?.Invoke(curCell[curX,curY]);
        playerInput.UI.Navigate.performed += Navigate;
        
    }

    protected virtual void Navigate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 pos = playerInput.UI.Navigate.ReadValue<Vector2>();
        curCell[curX,curY].OnDeselect();
        OnDeselectEvent?.Invoke(curCell[curX,curY]);
        
        curX += (int) pos.x;
        curY += (int) pos.y * -1;

        if (curX < 0) {
            curX = curCell.GetLength(0) - 1;
        } else if (curX > curCell.GetLength(0) - 1) {
            curX = 0;
        } else if (curY < 0) {
            curY =  curCell.GetLength(1) - 1;
        }  else if (curY > curCell.GetLength(1) - 1) {
            curY = 0;
        }

        Debug.Log(curX + "," +curY);

        curCell[curX,curY].OnSelect();
        OnSelectEvent?.Invoke(curCell[curX,curY]);
    }

    protected override void OnDisable() {
        // base.OnDisable();
        curCell[curX,curY].OnDeselect();
        OnDeselectEvent?.Invoke(curCell[curX,curY]);
        playerInput.UI.Navigate.performed -= Navigate;
    }
}
