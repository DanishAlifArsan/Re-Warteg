using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Weapon
{
    [SerializeField] private int healAmount;
    [SerializeField] private int maxConsume;
    private int numberOfConsume = 0;
    public override void Setup(PlayerAttack _playerAttack)
    {
        base.Setup(_playerAttack);
    }
    public override void Attack()
    {
        if (canAttack && PlayerHealth.instance.AddHealth(healAmount) && PlayerHealth.instance.coffeeAmount > 0)
        {
            canAttack = false;
            cooldownTimer = cooldown;
            attackEffect.Play();
            numberOfConsume++;
            PlayerHealth.instance.AddCoffee(-1);

            if (numberOfConsume > maxConsume)
            {
                PlayerHealth.instance.DecreaseHealth(10);
            }
            // todo jalankan animasi serangan
        }
    }
}
