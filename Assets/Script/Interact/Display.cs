using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Display : MonoBehaviour, Interactable
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera displayCamera;
    [SerializeField] ServeFood serveFoodUI; 
    [SerializeField] PlayerInteract player;
    PlayerInput playerInput;

    // Start is called before the first frame update
    private void Start()
    {
        playerInput = InputManager.instance.playerInput;
        playerInput.UI.Cancel.performed += CancelInteract;
    }

    private void CancelInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        serveFoodUI.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        displayCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract() {
        if (player.itemInHand?.itemType == ItemType.Food)
        {
            Debug.Log(player.itemInHand.itemType);
            player.itemInHand = null;
        } else {
            // mainCamera.gameObject.SetActive(false);
            // displayCamera.gameObject.SetActive(true);
            // serveFoodUI.gameObject.SetActive(true);

            PlateManager.instance.TakeFood();
        }
    }

    public string FlavorText()
    {
        if (player.itemInHand?.itemType == ItemType.Food)
        {
            return "Place";
        } else {
            return "serve";
        } 
    }
}
