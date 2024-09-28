using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeFood : MonoBehaviour
{
    [SerializeField] SelectionListHorizontal selectionList;
    private PlayerInput playerInput;
    
    private void OnEnable() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Disable();
        playerInput.UI.Enable();
    }

    private void OnDisable() {
        playerInput.Player.Enable();
        playerInput.UI.Disable();
    }
}
