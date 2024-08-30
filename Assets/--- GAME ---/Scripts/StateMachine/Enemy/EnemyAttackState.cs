using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float pauseTimer;

    public EnemyAttackState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }


    public override void EnterState()
    {
        base.EnterState();

        if(Context.Enemy.Detection.IsPlayerDetected())
        {
            Context.Enemy.transform.LookAt(Context.Enemy.Detection.GetTargetPosition());
        }

        NextState = EnemyStateMachine.EEnemyState.ATTACK;

        Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0f);
        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Attack);
    }

    public override void UpdateState()
    {
        pauseTimer += Time.deltaTime;

        // Transition if player out of range
        if(Context.Enemy.Detection.IsPlayerDetected())
        {
            float distance = Vector3.Distance(Context.Enemy.Detection.GetTargetPosition(), Context.Enemy.transform.position);
            if(distance >= Context.Enemy.Data.ChaseDistance)
            {
                NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;
            }
        }

        // If player in range, wait before re-attacking
        if (pauseTimer > Context.Enemy.Data.PauseBetweenAttacksDuration )
        {
            NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;

            pauseTimer = 0f;
        }
    }

    public override void ExitState()
    {
        base.EnterState();
    }

}
