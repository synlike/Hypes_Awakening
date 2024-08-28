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

        if (Context.Enemy.CurrentHP > 0)
        {
            ApplyKnockback(CurrentAttackTaken.KnockbackAmount);
        }
        else
        {
            ApplyKnockback(CurrentAttackTaken.KnockbackAmount / 3f);
            NextState = EnemyStateMachine.EEnemyState.DEATH;
        }
    }
    public override void ExitState()
    {
        EnemyAnimationEvents.HitDone.Remove(OnHitDone);
        CurrentAttackTaken = null;
    }

    private void ApplyKnockback(float force)
    {
        EnemyBase enemy = Context.Enemy;

        Vector3 dir = (enemy.transform.position - CurrentAttackTaken.Origin.position).normalized;

        enemy.transform.LookAt(CurrentAttackTaken.Origin);

        enemy.rb.AddForce(dir * force, ForceMode.Impulse);

        enemy.Animator.SetTrigger(AnimatorStateHashes.Hit);
    }

    public override void UpdateState()
    {
    }

    private void OnHitDone()
    {
        NextState = EnemyStateMachine.EEnemyState.WANDER;
    }
}
