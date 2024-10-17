using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInput playerInput;
    public GameDevice activeGameDevice;
    public Action OnGameDeviceChanged;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
        playerInput = new PlayerInput();

        InputSystem.onActionChange += OnInputActionChange;
    }

    private void OnInputActionChange(object arg1, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed && arg1 is InputAction)
        {
            InputAction inputAction = arg1 as InputAction;
            if (inputAction.activeControl.device.displayName == "VirtualMouse")
            {
                return;
            }
            if (inputAction.activeControl.device is Gamepad)
            {
                if (activeGameDevice != GameDevice.Gamepad)
                {
                    ChangeActiveGameDevice(GameDevice.Gamepad);
                }
            } else {
                if (activeGameDevice != GameDevice.KeyboardMouse)
                {
                    ChangeActiveGameDevice(GameDevice.KeyboardMouse);
                }
            }
        }
    }

    private void ChangeActiveGameDevice(GameDevice gameDevice) {
        activeGameDevice = gameDevice;

        Cursor.visible = activeGameDevice == GameDevice.KeyboardMouse;

        Debug.Log(activeGameDevice);

        OnGameDeviceChanged?.Invoke();
    }

    private void Start() {
        if (GameManager.instance.currentSession == GameSession.Home)
        {
           playerInput.Player.Disable();
            playerInput.UI.Enable(); 
        } else {
            playerInput.Player.Enable();
            playerInput.UI.Disable();
        }
    }

    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }
}

public enum GameDevice
{
    KeyboardMouse,
    Gamepad,
}
