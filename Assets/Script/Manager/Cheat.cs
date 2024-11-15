using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cheat : MonoBehaviour
{
    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private PlayerAttack playerAttack;
    PlayerInput playerInput;
    private bool isActive = false;
    private float defaultSpeed, defaultAccel, defaultCycleRate, defaultTimeRate;
    
    // Start is called before the first frame update
    private void Start()
    {
        playerInput = InputManager.instance.playerInput;
        // playerInput.Cheat.Enable();
        playerInput.Cheat.Speed.performed += Speed;
        playerInput.Cheat.AddMoney.performed += AddMoney;
        playerInput.Cheat.RemoveMoney.performed += RemoveMoney;
        playerInput.Cheat.AddInventory.performed += AddInventory;
    }

    private void AddInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Inventory.instance.GenerateNewItem();
    }

    private void RemoveMoney(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        CurrencyManager.instance.AddCurrency(1000);
    }

    private void AddMoney(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        CurrencyManager.instance.RemoveCurrency(1000);
    }

    private void Speed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isActive)
        {
            isActive = false;

            playerAgent.speed = 2;
            playerAgent.acceleration = 3;
            if (GameManager.instance.currentSession == GameSession.Warteg)
            {
                TimeManager.instance.cycleRate = 5;
            } else {
                GardenManager.instance.timeRate = 15;
                // foreach (var item in playerAttack.weaponList)
                // {
                //     item.attack = 5;
                //     item.cooldown = 0.1f;
                // }
            }
        } else {
            isActive = true;

            playerAgent.speed = 10;
            playerAgent.acceleration = 10;

            if (GameManager.instance.currentSession == GameSession.Warteg)
            {
                TimeManager.instance.cycleRate = 10;
            } else {
                GardenManager.instance.timeRate = 60;
                // foreach (var item in playerAttack.weaponList)
                // {
                //     item.attack = 5;
                //     item.cooldown = 0.1f;
                // }
            }
        }
    }

    private void OnDestroy() {
        // playerInput.Cheat.Disable();
        playerInput.Cheat.Speed.performed -= Speed;
        playerInput.Cheat.AddMoney.performed -= AddMoney;
        playerInput.Cheat.RemoveMoney.performed -= RemoveMoney;
        playerInput.Cheat.AddInventory.performed -= AddInventory;
    }
}
