using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy/EnemyData", order = 1)]
public class EnemyData : EntityData
{
    [Title("Enemy Stats")]
    public int MaxHealth = 5;

    [Title("Patrol Infos")]
    public float PatrolPauseDuration = 4.0f;
    public float WalkSpeed = 3.0f;
    public float RunSpeed = 5.0f;

    [Title("Chasing Infos")]
    public bool UseMovementPrediction = true;
    [Range(-1f, 1f)]
    public float MovementPredictionTreshold = 0f;
    [Range(0.25f, 2f)]
    public float MovementPredictionTime = 1f;
    [Range(0.0f, 1f)]
    public float RepathingDelay = 0.1f;
    [Range(0.0f, 10f)]
    public float StoppingDistance = 2.0f;

    [Title("Attack Infos")]
    public float PauseBetweenAttacksDuration = 2.0f;
    public float ChaseDistance = 3.0f;

    [Title("Death Infos")]
    public bool CanResurrect = false;
    [ShowIf("CanResurrect")]
    public float DeathDuration = 5.0f;

}
