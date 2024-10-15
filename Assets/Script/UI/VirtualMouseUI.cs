using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class VirtualMouseUI : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    private VirtualMouseInput virtualMouseInput;
    private void Awake() {
        virtualMouseInput = GetComponent<VirtualMouseInput>();  
    }

    private void Start() {
        InputManager.instance.OnGameDeviceChanged += UpdateVisibility;
    }

    private void UpdateVisibility()
    {
        gameObject.SetActive(InputManager.instance.activeGameDevice == GameDevice.Gamepad);
    }

    private void Update() {
        transform.localScale = Vector3.one * (1 / canvas.localScale.x);
        transform.SetAsLastSibling();
    }

    private void LateUpdate() {
        Vector2 virtualMousePos = virtualMouseInput.virtualMouse.position.value;
        virtualMousePos.x = Mathf.Clamp(virtualMousePos.x, 0, Screen.width);
        virtualMousePos.y = Mathf.Clamp(virtualMousePos.y, 0, Screen.height);
        InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePos);
    }
}
