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
        mainCamera.gameObject.SetActive(false);
        displayCamera.gameObject.SetActive(true);
        serveFoodUI.gameObject.SetActive(true);
    }

    public string FlavorText()
    {
        return "Serve";
    }
}
