using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueThrowState : EnemyState
{
    private float pauseTimer;

    public RogueThrowState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        NextState = EnemyStateMachine.EEnemyState.ATTACK;

        Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0f);
        Context.Enemy.NavAgent.isStopped = true;
        Context.Enemy.NavAgent.velocity = Vector3.zero;
        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Throw);
    }

    public override void UpdateState()
    {
        pauseTimer += Time.deltaTime;

        // Transition if player out of range
        if (Context.Enemy.Detection.IsPlayerDetected())
        {
            float distance = Vector3.Distance(Context.Enemy.Detection.GetTargetPosition(), Context.Enemy.transform.position);
            if (distance >= Context.Enemy.Data.ChaseDistance)
            {
                NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;
            }

            Context.Enemy.transform.LookAt(Context.Enemy.Detection.GetTargetAnticaptedPosition());
        }

        // If player in range, wait before re-attacking
        if (pauseTimer > Context.Enemy.Data.PauseBetweenAttacksDuration)
        {
            pauseTimer = 0f;

            NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;
        }
    }

    public override void ExitState()
    {
        base.EnterState();
    }
}
