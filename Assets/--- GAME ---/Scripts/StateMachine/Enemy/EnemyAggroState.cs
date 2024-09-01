using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAggroState : EnemyState
{
    private EnemyBase enemy;
    private NavMeshAgent agent;
        
    private float animationVelocity = 0.5f;
    private float blendTimer = 0.0f;

    private float RepathingTimer = 0.0f;

    public EnemyAggroState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy = Context.Enemy;
        agent = enemy.NavAgent;

        NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;
        animationVelocity = 0.5f;
        blendTimer = 0.0f;

        agent.speed = Context.Enemy.Data.RunSpeed;
        agent.stoppingDistance = Context.Enemy.Data.StoppingDistance;

        //Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 1f);

        // DO A AGGRO STATE FOR DISTANCE ????? (MELEE MIGHT ALSO USE THIS)
        if (enemy.Detection.IsPlayerDetected())
        {
            enemy.NavAgent.SetDestination(enemy.Detection.GetTarget4DirPosition(enemy.transform.position));
        }
    }

    public override void UpdateState()
    {
        if (enemy.Detection.IsPlayerDetected())
        {

            Debug.DrawRay(enemy.transform.position, (enemy.Detection.GetTarget4DirPosition(enemy.transform.position) - enemy.transform.position), Color.red);

            Vector3 playerPosition = enemy.Detection.GetTargetPosition();

            float distance = Vector3.Distance(playerPosition, enemy.transform.position);

            if (distance > 15f) // distance to stop chase
            {
                enemy.Detection.EmptyTarget();
                NextState = EnemyStateMachine.EEnemyState.WANDER;
            }
            else if (distance > enemy.Data.ChaseDistance || (agent.remainingDistance >= 0.05f)) // distance to chase
            {

                enemy.transform.LookAt(playerPosition);
                if (RepathingTimer >= enemy.Data.RepathingDelay)
                {
                    enemy.NavAgent.isStopped = false;

                    if(!enemy.Data.UseMovementPrediction)
                    {
                        enemy.NavAgent.SetDestination(playerPosition);
                    }
                    else
                    {
                        //Context.Enemy.NavAgent.SetDestination(GetInterceptTargetPosition());
                        //Context.Enemy.NavAgent.SetDestination(Context.Enemy.Detection.GetTargetAnticipatedPosition());
                        enemy.NavAgent.SetDestination(enemy.Detection.GetTarget4DirPosition(enemy.transform.position));
                    }

                    RepathingTimer = 0.0f;
                }
                RepathingTimer += Time.deltaTime;
            }
            else
            {
                if(agent.remainingDistance < 0.05f)
                {
                    NextState = EnemyStateMachine.EEnemyState.ATTACK;
                }
            }

            Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, Context.Enemy.NavAgent.velocity.magnitude / Context.Enemy.Data.RunSpeed);
            //ManageAnimBlend();
        }

        //if(Context.Enemy.Detection.IsPlayerDetected())
        //{
        //    float distance = Vector3.Distance(Context.Enemy.Detection.GetTargetPosition(), Context.Enemy.transform.position);


        //    if (distance > 15f) // distance to stop chase
        //    {
        //        Context.Enemy.Detection.EmptyTarget();
        //        NextState = EnemyStateMachine.EEnemyState.WANDER;
        //    }
        //    else if (distance > 2f) // distance to attack
        //    {
        //        Context.Enemy.transform.LookAt(Context.Enemy.Detection.GetTargetPosition());
        //        Context.Enemy.transform.position = Vector3.MoveTowards(Context.Enemy.transform.position, Context.Enemy.Detection.GetTargetPosition(), Context.Enemy.Data.RunSpeed * Time.deltaTime);
        //    }
        //    else 
        //    {
        //        NextState = EnemyStateMachine.EEnemyState.ATTACK;
        //    }

        //    ManageAnimBlend();
        //}
    }

    private Vector3 GetInterceptTargetPosition()
    {
        Vector3 playerPosition = Context.Enemy.Detection.GetTargetPosition();

        Vector3 targetPosition = playerPosition
            + Context.Enemy.Detection.GetTargetAverageVelocity() * Context.Enemy.Data.MovementPredictionTime;
        Vector3 directionToTarget = (targetPosition - Context.Enemy.transform.position).normalized;
        Vector3 directionToPlayer = (playerPosition - Context.Enemy.transform.position).normalized;

        float dot = Vector3.Dot(directionToPlayer, directionToTarget);

        if(dot < Context.Enemy.Data.MovementPredictionTreshold)
        {
            targetPosition = playerPosition;
        }

        return targetPosition;
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
        RepathingTimer = 0.0f;
    }
}
