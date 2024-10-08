using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : IState
{
    private float cooldownTimer;
    public void EnterState(StateUser user, StateManager stateManager)
    {
        cooldownTimer = Mathf.Infinity;
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        MonsterAI monster = user as MonsterAI;
        cooldownTimer += Time.deltaTime;

        Collider player = monster.PlayerInSight();
        if(player != null) {
            if (cooldownTimer >= monster.attackCooldown) {
                cooldownTimer = 0;
                monster.DamagePlayer(player);  // damage player harusnya ada di animasi serangan 
                // monster.animator.SetTrigger("Attack");  
            } 
        } else {
            stateManager.SwitchState(monster, monster.idle);
        }
    }
}
