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
    [SerializeField] private Animator anim;
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
        playerInput.Player.HotkeyClean.performed += ToClean;
    }

    private void ToClean(InputAction.CallbackContext context)
    {
        if (PlateManager.instance.dirtyPlate.Count > 0)
        {
            Vector2 pos = PlateManager.instance.dirtyPlate[0].transform.position;
            agent.SetDestination(pos);
        }
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
         if (agent.remainingDistance > agent.stoppingDistance)
        {
            anim.SetBool("walk", true);
        } else {
            anim.SetBool("walk", false);
        }
    }
}
