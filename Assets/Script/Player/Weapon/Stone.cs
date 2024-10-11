using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Weapon
{
    [SerializeField] private StoneObject[] stoneObject;
    private int currentIndex;

    public override void Setup(PlayerHealth _health)
    {
        base.Setup(_health);
        foreach (var item in stoneObject)
        {
            item.Setup(transform, health, attack);
        }
        currentIndex = 0;
    }

    public override void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            cooldownTimer = cooldown;
            attackEffect.Play();
            // todo jalankan animasi serangan

            stoneObject[currentIndex].Throw();
            currentIndex++;
            if (currentIndex > stoneObject.Length - 1)
            {
                currentIndex = 0;
            }
        }
    }
}
