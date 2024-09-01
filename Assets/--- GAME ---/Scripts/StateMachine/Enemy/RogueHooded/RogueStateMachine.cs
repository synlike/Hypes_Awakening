using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueStateMachine : EnemyStateMachine
{
    public readonly GameEvent Throw = new();

    protected override void Awake()
    {
        States.Add(EEnemyState.WANDER, new EnemyWanderState(this, EEnemyState.WANDER));
        States.Add(EEnemyState.HIT, new EnemyHitState(this, EEnemyState.HIT));
        States.Add(EEnemyState.AGGRESSIVE, new EnemyAggroState(this, EEnemyState.AGGRESSIVE));
        States.Add(EEnemyState.ATTACK, new RogueThrowState(this, EEnemyState.ATTACK));
        States.Add(EEnemyState.DEATH, new EnemyDeathState(this, EEnemyState.DEATH));

        base.Awake();
    }

    public void OnThrow()
    {
        Throw.Invoke();
    }
}
