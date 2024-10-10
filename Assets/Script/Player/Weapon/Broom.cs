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
            if (EnemyInSight().GetComponent<MonsterAI>().Damage(attack))
            {
                health.ResetCounter(); 
            } else {
                health.SetNumberOfAttack();
            }
        }
    }

    private Collider EnemyInSight() {
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, LayerMask.GetMask("enemy"));
        Collider selectedItem = null;
        float minDistance = float.PositiveInfinity;
        foreach (var item in cols)
        {
            float dist = Vector3.Distance(attackPoint.position, item.transform.position);
            if (dist < minDistance)
            {
                selectedItem = item;
                minDistance = dist;
            }
        }
        return selectedItem;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
