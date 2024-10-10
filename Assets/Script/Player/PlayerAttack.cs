using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    private void Start()
    {
        playerInput = InputManager.instance.playerInput;
        playerInput.Dungeon.Attack.performed += Attack;
    }

    private void Attack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (EnemyInSight() != null)
        {
            DamageEnemy(); // damage enemy harusnya ada di animasi serangan
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEnemy() {
        if (EnemyInSight() != null)
        {
            Debug.Log("Attack enemy "+ EnemyInSight().name);
        }
    }

    public Collider EnemyInSight() {
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, LayerMask.GetMask("enemy"));
        if (cols.Length > 0) return cols[0];
        else return null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
