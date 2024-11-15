using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cheat : MonoBehaviour
{
    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private PlayerAttack playerAttack;
    
    // Start is called before the first frame update
    private void Start()
    {
        InputManager.instance.playerInput.Player.Cheat.performed += Cheating;
    }

    private void Cheating(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        playerAgent.speed = 10;
        playerAgent.acceleration = 10;

        if (GameManager.instance.currentSession == GameSession.Warteg)
        {
            TimeManager.instance.cycleRate = 10;
        } else {
            GardenManager.instance.timeRate = 60;
            foreach (var item in playerAttack.weaponList)
            {
                item.attack = 5;
                item.cooldown = 0.1f;
            }
        }
    }

    private void OnDestroy() {
        InputManager.instance.playerInput.Player.Cheat.performed -= Cheating;
    }
}
