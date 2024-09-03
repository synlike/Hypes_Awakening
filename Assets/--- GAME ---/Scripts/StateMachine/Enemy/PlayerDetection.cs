using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerDetection : MonoBehaviour
{
    [field: SerializeField] public EnemyData Data { get; private set; }
    private PlayerBase _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBase player))
        {
            _player = player;
        }
    }

    public bool IsPlayerDetected()
    {
        return _player != null;
    }

    public Vector3 GetTargetPosition()
    {
        if (_player == null)
            Debug.LogError("Player is null, check before using Get Target Position");

        return _player.transform.position;
    }

    public Vector3 GetTargetAnticipatedPosition()
    {
        Vector3 playerPosition = GetTargetPosition();

        Vector3 targetPosition = playerPosition
            + GetTargetAverageVelocity() * Data.MovementPredictionTime;
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;

        float dot = Vector3.Dot(directionToPlayer, directionToTarget);

        if (dot < Data.MovementPredictionTreshold)
        {
            targetPosition = playerPosition;
        }

        return targetPosition;
    }

    public Vector3 GetTarget4DirMinimumPosition(Vector3 enemyPos, float minDistance)
    {
        Vector3 newPosition = GetTarget4DirPosition(enemyPos);
        Vector3 targetPosition = GetTargetPosition();

        if(targetPosition.x != newPosition.x)  // Si pas d'obstacle sur pos
        {
            if (targetPosition.x > newPosition.x)
            {
                newPosition.x = targetPosition.x - minDistance;
            }
            else
            {
                newPosition.x = targetPosition.x + minDistance;
            }
        }
        else
        {
            if (targetPosition.z > newPosition.z)
            {
                newPosition.z = targetPosition.z - minDistance;
            }
            else
            {
                newPosition.z = targetPosition.z + minDistance;
            }
        }

        return newPosition;

    }

    public Vector3 GetTarget4DirPosition(Vector3 enemyPosition)
    {
        //Vector3 anticipatedPosition = GetTargetAnticipatedPosition();
        Vector3 targetPosition = GetTargetPosition();

        Vector3 newPosition = enemyPosition;

        if(Mathf.Abs(targetPosition.x - enemyPosition.x) < Mathf.Abs(targetPosition.z - enemyPosition.z))
        {
            newPosition.x = targetPosition.x;
        }
        else
        {
            newPosition.z = targetPosition.z;
        }

        return newPosition;
    }

    public Vector3 GetTargetAverageVelocity()
    {
        if (_player == null)
            Debug.LogError("Player is null, check before using Get Target Position");

        return _player.AverageVelocity;
    }

    public void EmptyTarget()
    {
        _player = null;
    }
}
