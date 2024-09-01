using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    private float deathTimer = 0f;

    public EnemyDeathState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        NextState = EnemyStateMachine.EEnemyState.DEATH;
        deathTimer = 0f;
        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Death);
    }

    public override void UpdateState()
    {
        deathTimer += Time.deltaTime;

        if(deathTimer >= Context.Enemy.Data.TimeBeforeDespawning)
        {
            Context.Enemy.transform.parent.gameObject.SetActive(false);
            NextState = EnemyStateMachine.EEnemyState.WANDER;
        }
    }

    public override void ExitState()
    {
    }
}
