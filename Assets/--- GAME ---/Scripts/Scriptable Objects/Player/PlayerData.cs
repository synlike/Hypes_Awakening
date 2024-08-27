using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Title("Player Stats")]
    public int MaxHealth = 5;

    [Title("Movement Parameters")]
    public float IdleSpeed = 0.0f;
    public float WalkSpeed = 3.0f;
    public float RunSpeed = 6.0f;

    [Title("Animation Parameters")]
    public float SmoothAnimationDuration = 0.2f;
    public float SmoothAnimationDurationMelee = 0.05f;
    public float BlockWeightLerpDuration = 0.2f;
    [Range(0, 1)] public float IdleAnimationTreshold = 0.0f;
    [Range(0, 1)] public float WalkAnimationTreshold = 0.5f;
    [Range(0, 1)] public float RunAnimationTreshold = 1.0f;
}
