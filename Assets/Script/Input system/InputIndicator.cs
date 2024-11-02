using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIndicator : MonoBehaviour
{
    [SerializeField] private GameObject keyboardIndicator;
    [SerializeField] private GameObject gamepadIndicator;

    private void LateUpdate() {
        if (keyboardIndicator != null)
        {
            keyboardIndicator.SetActive(InputManager.instance.activeGameDevice == GameDevice.KeyboardMouse);
        }

        if (gamepadIndicator != null)
        {
            gamepadIndicator.SetActive(InputManager.instance.activeGameDevice == GameDevice.Gamepad);
        }
    }
}
