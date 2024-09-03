using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyWanderState : EnemyState
{
    private List<Transform> waypoints = new List<Transform>();
    private Transform currentTarget;
    private int currentIndex = 0;
    private float patrolTimer = 0.0f;

    // No need because patrol is fixed points
    //private float UpdateSpeed = 0.1f; // Time between destination recalculation (to avoid doing it every frame)
    //private float UpdateTimer = 0.0f;

    private bool canPatrol = false;

    private bool destinationReached = false;

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
            currentTarget = waypoints[0];// GetNextWaypoint(); // because first is spawn point
            canPatrol = true;
        }

        //Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0.5f);
        Context.Enemy.NavAgent.speed = Context.Enemy.Data.WalkSpeed;
        Context.Enemy.NavAgent.stoppingDistance = 0f;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(canPatrol)
        {
            if (!destinationReached)
            {
                Context.Enemy.transform.LookAt(currentTarget);
                Context.Enemy.NavAgent.SetDestination(currentTarget.position);
            }
            else
            {
                patrolTimer += Time.deltaTime;

                if (patrolTimer >= Context.Enemy.Data.PatrolPauseDuration)
                {
                    currentTarget = GetNextWaypoint();
                    Context.Enemy.transform.LookAt(currentTarget);
                    //Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0.5f);
                    patrolTimer = 0.0f;
                    destinationReached = false;
                }
                else
                {
                    //Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, 0.0f);
                }
            }

            //Context.Enemy.Animator.SetFloat(AnimatorStateHashes.VelocityX, Context.Enemy.NavAgent.velocity.normalized.x);
            //Context.Enemy.Animator.SetFloat(AnimatorStateHashes.VelocityZ, Context.Enemy.NavAgent.velocity.normalized.z);
            Context.Enemy.Animator.SetFloat(AnimatorStateHashes.Velocity, Context.Enemy.NavAgent.velocity.magnitude / Context.Enemy.Data.RunSpeed);
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

        if (!destinationReached && other.TryGetComponent(out Waypoint waypoint)
            && waypoint.transform.position == currentTarget.position)
        {
            // Destination reached
            destinationReached = true;
        }
    }

}
