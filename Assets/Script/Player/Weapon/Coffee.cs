using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Weapon
{
    public override void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            cooldownTimer = cooldown;
            // attackEffect.Play();
            // todo jalankan animasi serangan
            Debug.Log("Weapon" + name);
        }
    }
}
