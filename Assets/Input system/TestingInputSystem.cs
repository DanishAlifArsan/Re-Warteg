using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    private Rigidbody rb;
    PlayerInput playerInput;
    private void Awake() {
        rb = GetComponent<Rigidbody>(); 

        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        playerInput.Player.Jump.performed += Jump;
    }

    private void FixedUpdate() {
        Vector2 input = playerInput.Player.Movement.ReadValue<Vector2>();
        float movementSpeed = 1f;
        rb.velocity = new Vector3(input.x * movementSpeed, rb.velocity.y, input.y * movementSpeed);
    }

    public void Jump(InputAction.CallbackContext context) {
        Debug.Log("Jump"); 
    }
}
