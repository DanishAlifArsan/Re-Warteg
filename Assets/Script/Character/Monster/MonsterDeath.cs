using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeath : IState
{
    public void EnterState(StateUser user, StateManager stateManager)
    {
        
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        MonsterAI monster = user as MonsterAI;
        monster.Death(); // death harusnya ada di animasi mati
    }
}
