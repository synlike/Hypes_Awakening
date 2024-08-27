using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [Title("Enemy Stats")]
    public int MaxHealth = 5;
}
