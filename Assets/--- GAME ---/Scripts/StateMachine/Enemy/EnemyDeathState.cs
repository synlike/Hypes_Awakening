using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        NextState = EnemyStateMachine.EEnemyState.DEATH;

        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Death);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }
}
