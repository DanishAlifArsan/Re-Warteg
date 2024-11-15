using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<Weapon> weaponList;
    public Camera cam;
    private int currentIndex;
    private Weapon currentWeapon;
    public PlayerInput playerInput;

    // Start is called before the first frame update
    private void Start()
    {
        currentIndex = 0;
        currentWeapon = weaponList[currentIndex];
        currentWeapon.Select();
        
        playerInput = InputManager.instance.playerInput;
        playerInput.Dungeon.Attack.performed += Attack;
        playerInput.Dungeon.WeaponNext.performed += WeaponNext;
        playerInput.Dungeon.WeaponPrev.performed += WeaponPrev;

        foreach (Weapon item in weaponList)
        {
            item.Setup(this);   
        }
    }

    private void WeaponPrev(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        currentWeapon.MoveIcon(2);
        currentWeapon.Deselect();
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = weaponList.Count - 1;
        }
        currentWeapon = weaponList[currentIndex];
        currentWeapon.MoveIcon(1);
        currentWeapon.Select();
    }

    private void WeaponNext(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        currentWeapon.MoveIcon(0);
        currentWeapon.Deselect();
        currentIndex++;
        if (currentIndex > weaponList.Count - 1) {
            currentIndex = 0;
        }
        currentWeapon = weaponList[currentIndex];
        currentWeapon.MoveIcon(1);
        currentWeapon.Select();
    }

    private void Attack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        currentWeapon.Attack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        playerInput.Dungeon.Attack.performed -= Attack;
        playerInput.Dungeon.WeaponNext.performed -= WeaponNext;
        playerInput.Dungeon.WeaponPrev.performed -= WeaponPrev;
    }
}
