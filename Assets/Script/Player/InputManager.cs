using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class InputManager : MonoBehaviour
{
    [SerializeField] private RectTransform virtualMouseUI;
    public static InputManager instance;
    public PlayerInput playerInput;
    public GameDevice activeGameDevice;
    public InputUser user;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
        playerInput = new PlayerInput();
        
        // Debug.Log(activeGameDevice);    
        // InputSystem.onEvent += OnDeviceChange;
    }

    // private void OnDestroy() {
    //     InputSystem.onEvent -= OnDeviceChange;
    // }
    InputDevice _lastDevice;
    private void OnDeviceChange(InputEventPtr eventPtr, InputDevice device)  {
        if (_lastDevice == device) return;

        if (eventPtr.type != StateEvent.Type) return;

        bool validPress = false;
        foreach (InputControl control in eventPtr.EnumerateChangedControls(device, 0.01F))
        {
            validPress = true;
            break;
        }
        if (validPress is false) return;

        if (device is Keyboard || device is Mouse)
        {
            if (activeGameDevice == GameDevice.KeyboardMouse) return;
            ChangeActiveGameDevice(GameDevice.KeyboardMouse);
        }
        else if (device is Gamepad)
        {
            if (activeGameDevice == GameDevice.Gamepad) return;
            ChangeActiveGameDevice(GameDevice.Gamepad);
        }
    }

    private void ChangeActiveGameDevice(GameDevice gameDevice) {
        activeGameDevice = gameDevice;
        Debug.Log(activeGameDevice);

        Cursor.visible = activeGameDevice == GameDevice.KeyboardMouse;
        Cursor.lockState = Cursor.visible? CursorLockMode.None : CursorLockMode.Confined;

        if (virtualMouseUI != null)
        {
            virtualMouseUI?.gameObject.SetActive(activeGameDevice == GameDevice.Gamepad);
        }

        // GameDeviceManager.instance.activeGameDevice = activeGameDevice;
        // try{
        //     virtualMouseUI?.gameObject.SetActive(activeGameDevice == GameDevice.Gamepad);
        // }
        // catch (System.Exception)
        // {
        //     Debug.Log("Error di sini");
        // }
    }

    // public void DeviceChange() {
    //     int control = PlayerPrefs.GetInt("control", 0); // ganti game device di setting
    //     if (control > 0)
    //     {
    //         ChangeActiveGameDevice(GameDevice.Gamepad);
    //     } else {
    //         ChangeActiveGameDevice(GameDevice.KeyboardMouse);
    //     }   
    // }

    private void Start() {
        // ChangeActiveGameDevice(GameDeviceManager.instance.activeGameDevice);  // setting supaya gamedevice sama seperti gamedevice yang terakhir kali aktif
        // InputSystem.onEvent += OnDeviceChange;
        // DeviceChange();
        ChangeActiveGameDevice(activeGameDevice);

        if (GameManager.instance.currentSession == GameSession.Dungeon)
        {
            playerInput.Player.Enable();
            playerInput.UI.Disable();
        } else {
            playerInput.Player.Disable();
            playerInput.UI.Enable(); 
        }
    }

    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }
}

// public enum GameDevice
// {
//     KeyboardMouse,
//     Gamepad,
// }

