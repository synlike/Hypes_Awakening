using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
    [field: SerializeField] public EnemyData Data { get; private set; }
    public Animator Animator { get; private set; }


    void Start()
    {
        Animator = GetComponent<Animator>();

        MaxHP = Data.MaxHealth;
        CurrentHP = MaxHP;
    }


    void Update()
    {

    }

    public override void ApplyDamage(int amount)
    {
        base.ApplyDamage(amount);

        // Anim

    }
}
