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
        NextState = EnemyStateMachine.EEnemyState.HIT;

        EnemyAnimationEvents.HitDone.Add(OnHitDone);
        if (Context.Enemy.CurrentHP > 0)
        {

            Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Hit);
            ApplyKnockback(Context.Enemy.CurrentAttackTaken.KnockbackAmount);
        }
        else
        {
            ApplyKnockback(Context.Enemy.CurrentAttackTaken.KnockbackAmount / 3f);
            NextState = EnemyStateMachine.EEnemyState.DEATH;
        }
    }
    public override void ExitState()
    {
        EnemyAnimationEvents.HitDone.Remove(OnHitDone);
        Context.Enemy.NullifyCurrentAttackTaken();
    }

    private void ApplyKnockback(float force)
    {
        EnemyBase enemy = Context.Enemy;

        Vector3 dir = (enemy.transform.position - Context.Enemy.CurrentAttackTaken.Origin.position).normalized;

        enemy.transform.LookAt(Context.Enemy.CurrentAttackTaken.Origin);

        enemy.rb.AddForce(Vector3.up * 50f, ForceMode.Impulse);
        enemy.rb.AddForce((dir * force), ForceMode.Impulse);

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
