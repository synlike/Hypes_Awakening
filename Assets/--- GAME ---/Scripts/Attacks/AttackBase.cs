using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AttackInfos
{
    public IDamageable Damageable;
    public Transform Origin { get; private set; }
    public int DamageAmount { get; private set; }
    public float KnockbackAmount { get; private set; }

    public AttackInfos(IDamageable damageable, Transform origin, int damageAmount, float knockbackAmount)
    {
        Damageable = damageable;
        Origin = origin;
        DamageAmount = damageAmount;
        KnockbackAmount = knockbackAmount;
    }
}

public class AttackBase : MonoBehaviour
{
    [field: SerializeField] public AttackData Data { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable hit))
        {
            EntityBase attackGiver = GetComponentInParent<EntityBase>();

            if (attackGiver is null)
                Debug.LogError("Error : Attack Giver is null");

            hit.ApplyDamage(new AttackInfos(hit, attackGiver.transform, Data.Damage, Data.KnockbackAmount));
        }
    }
}
