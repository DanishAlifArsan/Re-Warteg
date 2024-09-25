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

    // Start is called before the first frame update
    private void Start()
    {
        playerInput = InputManager.instance.playerInput;
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
        
    }
}
