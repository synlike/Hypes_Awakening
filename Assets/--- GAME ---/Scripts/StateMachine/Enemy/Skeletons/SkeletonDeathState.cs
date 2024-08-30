using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeathState : EnemyDeathState
{
    private float deathTimer = 0.0f;

    public SkeletonDeathState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        NextState = EnemyStateMachine.EEnemyState.DEATH;

        deathTimer = 0.0f;
        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Death);

        Context.Enemy.OnDeath();
    }

    public override void UpdateState()
    {
        deathTimer += Time.deltaTime;

        if (deathTimer >= Context.Enemy.Data.DeathDuration)
        {
            NextState = EnemyStateMachine.EEnemyState.RESURRECT;
        }
    }

    public override void ExitState()
    {
    }
}
