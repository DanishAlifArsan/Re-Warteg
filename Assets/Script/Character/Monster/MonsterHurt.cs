using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHurt : IState
{
    public void EnterState(StateUser user, StateManager stateManager)
    {
        MonsterAI monster = user as MonsterAI;
        monster.transform.Translate(Vector3.forward * -monster.knockback, Space.Self);
        // monster.rb.AddForce(Vector3.forward * -monster.knockback, ForceMode.Impulse);
    }

    public void UpdateState(StateUser user, StateManager stateManager)
    {
        MonsterAI monster = user as MonsterAI;
        stateManager.SwitchState(user, monster.idle);
    }
}
