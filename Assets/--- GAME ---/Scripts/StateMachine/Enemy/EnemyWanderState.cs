using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyWanderState : EnemyState
{
    private List<Transform> waypoints = new List<Transform>();
    private Transform currentTarget;
    private int currentIndex = 0;
    private float patrolTimer = 0.0f;

    private bool canPatrol = false;

    public EnemyWanderState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        NextState = EnemyStateMachine.EEnemyState.WANDER;
        base.EnterState();

        waypoints = Context.Enemy.Patrolling.Waypoints;

        if (waypoints.Count > 0)
        {
            currentTarget = waypoints[currentIndex];
            canPatrol = true;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(canPatrol)
        {
            Vector3 position = Context.Enemy.transform.position;
            position.y = 0f;

            if (position != currentTarget.position)
            {
                Context.Enemy.transform.position = Vector3.MoveTowards(position, currentTarget.position, Context.Enemy.Data.WalkSpeed * Time.deltaTime);
            }
            else
            {
                patrolTimer += Time.deltaTime;

                if (patrolTimer >= Context.Enemy.Data.PatrolPauseDuration)
                {
                    currentTarget = GetNextWaypoint();
                    Context.Enemy.transform.LookAt(currentTarget);
                    Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0.5f);
                    patrolTimer = 0.0f;
                }
                else
                {
                    Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0.0f);
                }
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private Transform GetNextWaypoint()
    {
        currentIndex++;

        if(currentIndex >= waypoints.Count)
        {
            currentIndex = 0;
        }
        
        return waypoints[currentIndex];
    }


    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBase player))
        {
            NextState = EnemyStateMachine.EEnemyState.AGGRESSIVE;
        }
    }

}
