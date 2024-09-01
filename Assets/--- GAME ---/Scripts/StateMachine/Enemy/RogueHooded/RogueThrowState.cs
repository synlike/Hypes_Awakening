using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RogueThrowState : EnemyState
{
    private float pauseTimer;
    private bool isThrowing = false;

    public RogueThrowState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        NextState = EnemyStateMachine.EEnemyState.ATTACK;

        ((RogueStateMachine)Context).Throw.Add(OnThrow);

        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Throw);
    }

    private void OnThrow()
    {
        isThrowing = true;
    }

    public override void UpdateState()
    {
        pauseTimer += Time.deltaTime;

        // Transition if player out of range
        if (Context.Enemy.Detection.IsPlayerDetected())
        {
            if (!isThrowing)
            {
                Context.Enemy.NavAgent.SetDestination(Context.Enemy.Detection.GetTarget4DirPosition(Context.Enemy.transform.position));
            }
            else
            {
                Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0f);
                Context.Enemy.NavAgent.isStopped = true;
                Context.Enemy.NavAgent.velocity = Vector3.zero;

                float distance = Vector3.Distance(Context.Enemy.Detection.GetTargetPosition(), Context.Enemy.transform.position);
                if (distance >= Context.Enemy.Data.ChaseDistance)
                {
                    NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;
                }
            }
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
        isThrowing = false;
        ((RogueStateMachine)Context).Throw.Remove(OnThrow);
    }
}
