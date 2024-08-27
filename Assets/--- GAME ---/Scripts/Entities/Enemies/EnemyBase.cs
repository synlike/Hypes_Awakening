using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
    [field: SerializeField] public EnemyData Data { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody rb { get; private set; }

    void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        MaxHP = Data.MaxHealth;
        CurrentHP = MaxHP;
    }


    void Update()
    {

    }

    public override void ApplyDamage(AttackInfos attackInfos)
    {
        base.ApplyDamage(attackInfos);

        EnemyEvents.Hit.Invoke(attackInfos);
    }

    public override void OnDie()
    {
        base.OnDie();
    }

    public void HitDone()
    {

    }
}
