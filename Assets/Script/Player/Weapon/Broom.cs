using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Broom : Weapon
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    public override void Attack() {
        Vector2 mousePosition = playerAttack.playerInput.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = playerAttack.cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (canAttack && Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("enemy")))
        {
            if (EnemyInSight().Contains(hit.collider))
            {
                Vector3 targetPos = new Vector3( hit.transform.position.x, playerAttack.transform.position.y, hit.transform.position.z ) ;
                playerAttack.transform.LookAt(targetPos);

                canAttack = false;
                cooldownTimer = cooldown;
                attackEffect.Play();
                // todo jalankan animasi serangan

                if (hit.transform.GetComponent<MonsterAI>().Damage(attack))
                {
                    // health.ResetCounter(); 
                    PlayerHealth.instance.ResetCounter(); 
                } else {
                    // health.SetNumberOfAttack();
                    PlayerHealth.instance.SetNumberOfAttack();
                }
            } 
        }
    }

    private Collider[] EnemyInSight() {
        return Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("enemy"));;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
