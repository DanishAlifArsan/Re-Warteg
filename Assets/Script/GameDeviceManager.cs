using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeviceManager : MonoBehaviour
{
    public static GameDeviceManager instance;
    public GameDevice activeGameDevice;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(instance);
    }
}

public enum GameDevice
{
    KeyboardMouse,
    Gamepad,
}