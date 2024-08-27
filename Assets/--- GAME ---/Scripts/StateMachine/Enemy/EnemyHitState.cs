using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        EnemyAnimationEvents.HitDone.Add(OnHitDone);
        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Hit);

        ApplyKnockback();
    }
    public override void ExitState()
    {
        EnemyAnimationEvents.HitDone.Remove(OnHitDone);
        CurrentAttackTaken = null;
    }

    private void ApplyKnockback()
    {

    }

    public override void UpdateState()
    {
    }

    private void OnHitDone()
    {
        NextState = EnemyStateMachine.EEnemyState.WANDER;
    }
}
