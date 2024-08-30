using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAggroState : EnemyState
{
    private float animationVelocity = 0.5f;
    private float blendTimer = 0.0f;

    public EnemyAggroState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;
        animationVelocity = 0.5f;
        blendTimer = 0.0f;

        //Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 1f);
    }

    public override void UpdateState()
    {
        if(Context.Enemy.Detection.IsPlayerDetected())
        {
            float distance = Vector3.Distance(Context.Enemy.Detection.GetTargetPosition(), Context.Enemy.transform.position);


            if (distance > 15f) // distance to stop chase
            {
                Context.Enemy.Detection.EmptyTarget();
                NextState = EnemyStateMachine.EEnemyState.WANDER;
            }
            else if (distance > 2f) // distance to attack
            {
                Context.Enemy.transform.LookAt(Context.Enemy.Detection.GetTargetPosition());
                Context.Enemy.transform.position = Vector3.MoveTowards(Context.Enemy.transform.position, Context.Enemy.Detection.GetTargetPosition(), Context.Enemy.Data.RunSpeed * Time.deltaTime);
            }
            else 
            {
                NextState = EnemyStateMachine.EEnemyState.ATTACK;
            }

            ManageAnimBlend();
        }
    }

    private void ManageAnimBlend()
    {
        if (animationVelocity != 1f)
        {
            animationVelocity = Mathf.Lerp(0.5f, 1f, blendTimer / 0.3f);

            blendTimer += Time.deltaTime;

            if (blendTimer >= 0.3f)
            {
                animationVelocity = 1f;
            }

            Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, animationVelocity);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
