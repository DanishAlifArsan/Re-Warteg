using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputText : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private string keyboardText;
    [SerializeField] private string gamepadText;
    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate() {
        if (InputManager.instance.activeGameDevice == GameDevice.Gamepad)
        {
            text.text = gamepadText;
        } else {
            text.text = keyboardText; 
        }
    }
}
