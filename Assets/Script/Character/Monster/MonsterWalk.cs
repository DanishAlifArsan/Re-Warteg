using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalk : IState
{
    private Collider player;
    public void EnterState(StateUser user, StateManager stateManager)
    {
        MonsterAI monster = user as MonsterAI;

        if (monster.isChasing && monster.pool.DetectPlayer() != null)
        {
            player = monster.pool.DetectPlayer();
        } else {
            monster.agent.SetDestination(monster.pool.transform.position);
        }
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        MonsterAI monster = user as MonsterAI;

        if (monster.pool.AggroPlayer())
        {
            monster.agent.SetDestination(player.transform.position);
        } else {
            monster.isChasing = false;
            monster.isWaiting = true;
            stateManager.SwitchState(monster, monster.idle);
        }
        if (monster.PlayerInSight() != null)
        {
            monster.isWaiting = true;
            stateManager.SwitchState(monster, monster.attack);
        }
    }
}