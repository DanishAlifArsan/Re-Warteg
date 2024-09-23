using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] private NavMeshAgent agent;

    private void Awake() {
        playerInput = new PlayerInput();
    }

    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerInput.Mouse.MouseClick.performed += OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = playerInput.Mouse.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
