using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerBase : EntityBase
{
    [field: SerializeField] public PlayerData Data { get; private set; }

    public CharacterController CharacterController { get; private set; }
    public PlayerInputManager Inputs { get; private set; }
    public Animator Animator { get; private set; }

    // FOR AI CHASING
    [SerializeField]
    [Range(0.1f, 5f)]
    private float HistoricalPositionDuration = 1f;
    [SerializeField]
    [Range(0.001f, 1f)]
    private float HistoricalPositionInterval = 0.1f;

    public Vector3 AverageVelocity 
    { 
        get
        {
            Vector3 average = Vector3.zero;
            foreach(Vector3 velocity in HistoricalVelocities)
            {
                average += velocity;
            }
            average.y = 0f;

            return average / HistoricalVelocities.Count;
        }
    }

    private Queue<Vector3> HistoricalVelocities;
    private float LastPositionTime;
    private int MaxQueueSize;

    // END AI CHASING

    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Inputs = GetComponent<PlayerInputManager>();
        Animator = GetComponent<Animator>();

        MaxHP = Data.MaxHealth;
        CurrentHP = MaxHP;

        MaxQueueSize = Mathf.CeilToInt(1f / HistoricalPositionInterval * HistoricalPositionDuration);
        HistoricalVelocities = new Queue<Vector3>(MaxQueueSize);
    }

    private void OnDestroy()
    {
    }

    void Update()
    {
        if(LastPositionTime + HistoricalPositionInterval <= Time.time)
        {
            if(HistoricalVelocities.Count > MaxQueueSize)
            {
                HistoricalVelocities.Dequeue();
            }

            HistoricalVelocities.Enqueue(CharacterController.velocity);
            LastPositionTime = Time.time;
        }
    }
}
