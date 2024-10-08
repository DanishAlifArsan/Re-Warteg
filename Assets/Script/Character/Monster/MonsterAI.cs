using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour, StateUser
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    public float idleDuration;
    public float attackCooldown;
    public NavMeshAgent agent;
    public MonsterPool pool;
    private StateManager stateManager;
    public bool isChasing;
    public bool isWaiting;

    private void OnEnable() {
        isChasing = false;
        isWaiting = false;
        stateManager = new StateManager();
        stateManager.StartState(this, idle);
    }

    private void OnDisable() {
        stateManager = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        stateManager.currentState.UpdateState(this, stateManager);
    }

    public MonsterIdle idle = new MonsterIdle();
    public MonsterWalk walk = new MonsterWalk();
    public MonsterAttack attack = new MonsterAttack();
    public MonsterDeath death = new MonsterDeath();
    
    public Collider PlayerInSight() {
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, LayerMask.GetMask("player"));
        if (cols.Length > 0) return cols[0];
        else return null;
    }

    public void DamagePlayer(Collider player) {
        if (PlayerInSight()) {
            //damage player
            Debug.Log("Damage player " + player.name);
        }
    }

    public void Death() { // panggil saat mati
        pool.Despawn(this);
    }

    public void Damage() { // panggil ketika monster kena serangan dan nyawanya habis
        stateManager.SwitchState(this, death);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
