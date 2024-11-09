using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeath : IState
{
    public void EnterState(StateUser user, StateManager stateManager)
    {
        MonsterAI monster = user as MonsterAI;
        monster.anim.SetTrigger("death");
        AudioManager.instance.PlaySound(monster.deathSound);
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        
    }
}
