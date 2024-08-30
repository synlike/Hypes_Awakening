using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateMachine : EnemyStateMachine
{
    protected override void Awake()
    {
        States.Add(EEnemyState.WANDER, new EnemyWanderState(this, EEnemyState.WANDER));
        States.Add(EEnemyState.HIT, new EnemyHitState(this, EEnemyState.HIT));
        States.Add(EEnemyState.AGGRESSIVE, new EnemyAggroState(this, EEnemyState.AGGRESSIVE));
        States.Add(EEnemyState.ATTACK, new EnemyAttackState(this, EEnemyState.ATTACK));
        States.Add(EEnemyState.DEATH, new SkeletonDeathState(this, EEnemyState.DEATH));
        States.Add(EEnemyState.RESURRECT, new SkeletonResurrectState(this, EEnemyState.RESURRECT));

        base.Awake();
    }
}
