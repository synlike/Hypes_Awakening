using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonResurrectState : EnemyState
{
    public SkeletonResurrectState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        EnemyAnimationEvents.ResurrectDone.Add(OnResurrectionDone);

        Context.Enemy.Animator.SetTrigger(AnimatorStateHashes.Resurrect);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        EnemyAnimationEvents.ResurrectDone.Add(OnResurrectionDone);
    }

    private void OnResurrectionDone()
    {
        // Reactivate colliders and all
        Context.Enemy.OnResurrection();

        NextState = EnemyStateMachine.EEnemyState.WANDER;
    }
}
