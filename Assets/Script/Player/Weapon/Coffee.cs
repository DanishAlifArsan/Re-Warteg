using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Weapon
{
    [SerializeField] private int healAmount;
    [SerializeField] private int maxConsume;
    private int numberOfConsume = 0;
    public override void Attack()
    {
        if (canAttack && health.AddHealth(healAmount))
        {
            canAttack = false;
            cooldownTimer = cooldown;
            attackEffect.Play();
            numberOfConsume++;

            if (numberOfConsume > maxConsume)
            {
                health.DecreaseHealth(10);
            }
            // todo jalankan animasi serangan
        }
    }
}
