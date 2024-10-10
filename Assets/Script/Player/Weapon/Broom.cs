using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Weapon
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    public override void Attack() {
        if (canAttack)
        {
            canAttack = false;
            cooldownTimer = cooldown;
            attackEffect.Play();
            // todo jalankan animasi serangan
            DamageEnemy();
        }
    }

    public void DamageEnemy() {     // harusnya ini di animasi serangan
        if (EnemyInSight() != null)
        {
            EnemyInSight().GetComponent<MonsterAI>().Damage(attack);
        }
    }

    private Collider EnemyInSight() {
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, LayerMask.GetMask("enemy"));
        if (cols.Length > 0) return cols[0];
        else return null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
