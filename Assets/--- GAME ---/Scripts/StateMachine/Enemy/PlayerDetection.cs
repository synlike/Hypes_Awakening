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

    public Vector3 GetTargetAnticaptedPosition()
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
