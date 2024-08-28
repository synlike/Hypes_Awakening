using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBase : EnemyBase
{
    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();
    }

    public override void ApplyDamage(AttackInfos attackInfos)
    {
        base.ApplyDamage(attackInfos);
    }

    public override void OnResurrection()
    {
        CurrentHP = MaxHP;
        myCollider.enabled = true;
        rb.isKinematic = false;
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }
}
