using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera cam;
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
        playerInput.Kitchen.HotkeyKitchen.performed += ToKitchen;
        playerInput.Kitchen.HotkeyDisplay.performed += ToDisplay;
        playerInput.Kitchen.HotkeyClean.performed += ToClean;
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
        Vector2 mousePosition = InputManager.instance.activeGameDevice == GameDevice.KeyboardMouse? 
            playerInput.Player.MousePosition.ReadValue<Vector2>() : playerInput.Player.VirtualMouse.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.point + " "+  mousePosition);
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
