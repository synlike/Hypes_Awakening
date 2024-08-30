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

    [Title("Death Infos")]
    public bool CanResurrect = false;
    [ShowIf("CanResurrect")]
    public float DeathDuration = 5.0f;

}
