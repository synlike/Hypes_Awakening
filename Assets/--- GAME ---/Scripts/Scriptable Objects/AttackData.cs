using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/Attacks/AttackData", order = 1)]
public class AttackData : ScriptableObject
{
    public float Damage = 0f;
    public float KnockbackAmount = 0;

    public bool IsProjectile = false;
    [ShowIf("IsProjectile")]
    public float ProjectileSpeed = 8f;
}
