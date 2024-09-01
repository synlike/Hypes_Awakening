using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : EntityBase
{
    [field: SerializeField] public EnemyData Data { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
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
        NavAgent = GetComponent<NavMeshAgent>();

        MaxHP = Data.MaxHealth;
        CurrentHP = MaxHP;
    }


    protected virtual void Update()
    {

    }

    public override void EnableAttack(EAttack_Types attack_type)
    {
        base.EnableAttack(attack_type);
    }

    public override void ApplyDamage(AttackInfos attackInfos)
    {
        base.ApplyDamage(attackInfos);
    }

    public virtual void OnResurrection(){}

    public override void InitializeSpawnable()
    {
        base.InitializeSpawnable();
        DisablePhysics();
    }

    public override void OnDeath()
    {
        base.OnDeath();

        rb.isKinematic = true;
        myCollider.enabled = false;
    }
    public override void EnablePhysics()
    {
        NavAgent.enabled = false;
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public override void DisablePhysics()
    {
        if(rb is not null && NavAgent is not null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            NavAgent.Warp(transform.position);
            NavAgent.enabled = true;
        }
    }

    public override void ApplyKnockback(float force)
    {
        EnablePhysics();

        Vector3 dir = (transform.position - CurrentAttackTaken.Origin.position).normalized;

        transform.LookAt(CurrentAttackTaken.Origin);

        rb.AddForce(Vector3.up * 50f, ForceMode.Impulse);
        rb.AddForce((dir * force), ForceMode.Impulse);

        Animator.SetTrigger(AnimatorStateHashes.Hit);
    }
}
