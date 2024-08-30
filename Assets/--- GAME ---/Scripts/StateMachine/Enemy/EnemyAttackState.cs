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
        NextState = EnemyStateMachine.EEnemyState.ATTACK;

        Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0f);
        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Attack);
    }

    public override void UpdateState()
    {
        pauseTimer += Time.deltaTime;

        if(pauseTimer > Context.Enemy.Data.PauseBetweenAttacksDuration )
        {
            NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;

            pauseTimer = 0f;
        }


    }

    public override void ExitState()
    {
    }

}
