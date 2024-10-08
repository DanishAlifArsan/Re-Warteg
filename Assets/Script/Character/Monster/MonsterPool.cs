using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] private List<MonsterAI> monster;
    [SerializeField] private float detectRange;
    [SerializeField] private float aggroRange;
    [SerializeField] private float spawnTime;
    private float spawnCooldown = 0;
    private bool isSpawned;
    public List<MonsterAI> spawnedMonster = new List<MonsterAI>();

    // Start is called before the first frame update
    private void Start()
    {
        Spawn(false);
    }   

    // Update is called once per frame
    private void Update()
    {
        if (isSpawned)
        {
            return;
        }

        spawnCooldown += Time.deltaTime;
        if (spawnCooldown >= spawnTime)
        {
            Spawn(true);
            spawnCooldown = 0;
        }
    }

    private void Spawn(bool status) {
        foreach (MonsterAI item in monster)
        {
            item.gameObject.SetActive(status);
            if (status) {
                spawnedMonster.Add(item);
                item.pool = this;
            }
        }
        isSpawned = status;
    }

    public void Despawn(MonsterAI monster) {
        if (spawnedMonster.Contains(monster))
        {
            monster.gameObject.SetActive(false);
            spawnedMonster.Remove(monster);
            if (spawnedMonster.Count == 0)
            {
                isSpawned = false;
            }
        }
    }

    public Collider DetectPlayer() {
        Collider[] cols = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("player"));
        if (cols.Length > 0) return cols[0];
        else return null;
    }
    public bool AggroPlayer() {
        Collider[] cols = Physics.OverlapSphere(transform.position, aggroRange, LayerMask.GetMask("player"));
        return cols.Length > 0;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, detectRange);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, aggroRange);
    }   
}
