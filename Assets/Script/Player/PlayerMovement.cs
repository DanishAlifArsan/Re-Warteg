using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    // public ThirdPersonCharacter character;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private Kitchen kitchen;
    [SerializeField] private Display display;

    // Start is called before the first frame update
    private void Start()
    {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.MouseClick.performed += OnMouseClick;
        playerInput.Player.HotkeyKitchen.performed += ToKitchen;
        playerInput.Player.HotkeyDisplay.performed += ToDisplay;
    }

    private void ToDisplay(InputAction.CallbackContext context)
    {
        agent.SetDestination(display.transform.position);
    }

    private void ToKitchen(InputAction.CallbackContext context)
    {
        agent.SetDestination(kitchen.transform.position);
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = playerInput.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }

        // if (agent.remainingDistance > agent.stoppingDistance)
        // {
        //     character.Move(agent.desiredVelocity, false, false);
        // } else {
        //     character.Move(Vector3.zero, false, false);
        // }
    }

    // Update is called once per frame
    void Update()
    {
         // if (agent.remainingDistance > agent.stoppingDistance)
        // {
        //     character.Move(agent.desiredVelocity, false, false);
        // } else {
        //     character.Move(Vector3.zero, false, false);
        // }
    }
}
