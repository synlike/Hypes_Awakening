using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
    [field: SerializeField] public EnemyData Data { get; private set; }
    public EnemyPatrolling Patrolling { get; private set; }
    public PlayerDetection Detection { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody rb { get; private set; }


    protected Collider myCollider;

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        Patrolling = GetComponent<EnemyPatrolling>();
        Detection = GetComponentInChildren<PlayerDetection>();

        MaxHP = Data.MaxHealth;
        CurrentHP = MaxHP;
    }


    protected virtual void Update()
    {

    }

    public override void ApplyDamage(AttackInfos attackInfos)
    {
        base.ApplyDamage(attackInfos);
    }

    public virtual void OnResurrection(){}

    public override void OnDeath()
    {
        base.OnDeath();

        rb.isKinematic = true;
        myCollider.enabled = false;
    }
}
