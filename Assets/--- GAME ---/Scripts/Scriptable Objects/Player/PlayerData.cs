using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float IdleSpeed { get; private set; } = 0.0f;
    [field: SerializeField] public float WalkSpeed { get; private set; } = 3.0f;
    [field: SerializeField] public float BlockSpeed { get; private set; } = 4.0f;
    [field: SerializeField] public float RunSpeed { get; private set; } = 6.0f;
    [field: SerializeField] public float SmoothAnimationTime { get; private set; } = 0.2f;
    [field: SerializeField] public float SmoothAnimationTimeMelee { get; private set; } = 0.05f;
    [field: SerializeField] public float IdleAnimationTreshold { get; private set; } = 0.0f;
    [field: SerializeField] public float WalkAnimationTreshold { get; private set; } = 0.5f;
    [field: SerializeField] public float RunAnimationTreshold { get; private set; } = 1.0f;
    [field: SerializeField] public float TimeToBlockWeightLerp { get; private set; } = 0.2f;
}
