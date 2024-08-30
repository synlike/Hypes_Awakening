using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAggroState : EnemyState
{
    public EnemyAggroState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;

        Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 1f);
    }

    public override void UpdateState()
    {
        if(Context.Enemy.Detection.Player != null)
        {
            float distance = Vector3.Distance(Context.Enemy.Detection.Player.transform.position, Context.Enemy.transform.position);


            if (distance > 15f) // distance to stop chase
            {
                NextState = EnemyStateMachine.EEnemyState.WANDER;
            }
            else if (distance > 2f) // distance to attack
            {
                Context.Enemy.transform.LookAt(Context.Enemy.Detection.Player.transform);
                Context.Enemy.transform.position = Vector3.MoveTowards(Context.Enemy.transform.position, Context.Enemy.Detection.Player.transform.position, Context.Enemy.Data.RunSpeed * Time.deltaTime);
            }
            else 
            {
                NextState = EnemyStateMachine.EEnemyState.ATTACK;
            }
        }
    }

    public override void ExitState()
    {
    }
}
