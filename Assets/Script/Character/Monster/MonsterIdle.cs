using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : IState
{
    private float idleTimer = 0f;
    public void EnterState(StateUser user, StateManager stateManager)
    {
        
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        MonsterAI monster = user as MonsterAI;
        if (monster.pool.DetectPlayer() != null)
        {
            monster.isWaiting = false;  
            idleTimer = 0f;
            monster.isChasing = true;
            stateManager.SwitchState(monster, monster.walk);
        }

        if (monster.PlayerInSight())
        {
            monster.isWaiting = false;  
            idleTimer = 0f;
            stateManager.SwitchState(monster, monster.attack);
        }

        if (monster.isWaiting)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= monster.idleDuration)
            {
                idleTimer = 0f;
                monster.isWaiting = false;  
                stateManager.SwitchState(monster, monster.walk);
            }
        }
    }
}
