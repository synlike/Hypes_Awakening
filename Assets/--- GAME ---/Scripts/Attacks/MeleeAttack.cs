using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackBase
{

    public override void EnableAttack(EntityBase origin)
    {

    }

    public override void DisableAttack()
    {
        gameObject.SetActive(false);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
