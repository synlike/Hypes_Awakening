using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfos
{
    public IDamageable Damageable { get; private set; }
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
            PlayerBase player = GetComponentInParent<PlayerBase>();

            if (player is null)
                Debug.LogError("Error : Player is null");

            hit.ApplyDamage(new AttackInfos(hit, player.transform, Data.Damage, Data.KnockbackAmount));
        }
    }
}
