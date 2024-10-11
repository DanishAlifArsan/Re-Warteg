using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour, StateUser
{
    public GameObject healthBar;
    [SerializeField] Image fullHealthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private DropItem dropItem;
    [SerializeField] private int dropAmount;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    public float idleDuration;
    public float attackCooldown;
    // public float knockback;
    public Transform spawnPos;
    public NavMeshAgent agent;
    public MonsterPool pool;
    private StateManager stateManager;
    public bool isChasing;
    public bool isWaiting;
    private float currentHealth;

    private void OnEnable() {
        currentHealth = maxHealth;
        isChasing = false;
        isWaiting = false;
        stateManager = new StateManager();
        stateManager.StartState(this, idle);

        fullHealthBar.fillAmount = 1;
        healthBar.SetActive(true);
    }

    private void OnDisable() {
        stateManager = null;

        healthBar.SetActive(false);
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
    public MonsterHurt hurt = new MonsterHurt();
    
    public Collider PlayerInSight() {
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRange, LayerMask.GetMask("player"));
        if (cols.Length > 0) return cols[0];
        else return null;
    }

    public void DamagePlayer() {
        if (PlayerInSight() != null) {
            //damage player
            // PlayerInSight().transform.Translate(Vector3.forward * -knockback, Space.Self);
            PlayerInSight().GetComponent<PlayerHealth>().SetHitRecieved();
        }
    }

    public void Death() { // panggil saat mati
        Inventory.instance.AddItem(dropItem, dropAmount);
        GardenManager.instance.Pickup(dropItem, dropAmount);
        pool.Despawn(this);
    }

    public bool Damage(float damage) { // panggil ketika monster kena serangan dan nyawanya habis
        currentHealth -= damage;
        fullHealthBar.fillAmount = currentHealth/maxHealth;
        if (currentHealth <= 0)
        {
            stateManager.SwitchState(this, death);
            return true;
        } else {
            stateManager.SwitchState(this, hurt);
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
