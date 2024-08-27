using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/Attacks/AttackData", order = 1)]
public class AttackData : ScriptableObject
{
    public int Damage = 0;
    public float KnockbackAmount = 0;
}
