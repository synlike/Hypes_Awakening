using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    private float knockbackTime = 0.0f;
    private bool knockback = false;

    public EnemyHitState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        NextState = EnemyStateMachine.EEnemyState.HIT;

        Context.Enemy.NavAgent.isStopped = true;

        EnemyAnimationEvents.HitDone.Add(OnHitDone);
        if (Context.Enemy.CurrentHP > 0)
        {

            Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Hit);
            Context.Enemy.ApplyKnockback(Context.Enemy.CurrentAttackTaken.KnockbackAmount);
        }
        else
        {
            Context.Enemy.ApplyKnockback(Context.Enemy.CurrentAttackTaken.KnockbackAmount);
            NextState = EnemyStateMachine.EEnemyState.DEATH;
        }
    }

    public override void UpdateState()
    {
        if(knockback)
        {
            knockbackTime = Time.time;

            if (Context.Enemy.rb.velocity.magnitude < 0.05f || Time.time > knockbackTime + 1f)
            {
                knockback = false;
            }
        }
    }

    public override void ExitState()
    {
        EnemyAnimationEvents.HitDone.Remove(OnHitDone);
        Context.Enemy.NullifyCurrentAttackTaken();
    }


    private void OnHitDone()
    {
        Context.Enemy.DisablePhysics();
        NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;
    }
}
