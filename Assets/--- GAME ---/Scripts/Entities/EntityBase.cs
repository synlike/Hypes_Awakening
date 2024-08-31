using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour, IDamageable, IHealth
{
    [field: SerializeField] public MeleeAttack MeleeAttack { get; private set; }
    [field: SerializeField] public ThrowAttack ThrowAttack { get; private set; }

    private float _currentHP;
    private float _maxHP;
    private AttackInfos _currentAttackTaken;

    public float CurrentHP { get => _currentHP; set => _currentHP = value; }
    public float MaxHP { get => _maxHP; set => _maxHP = value; }
    public AttackInfos CurrentAttackTaken { get => _currentAttackTaken; private set => _currentAttackTaken = value; }

    public virtual void ApplyDamage(AttackInfos attackInfos)
    {
        if(CurrentAttackTaken == null)
        {
            CurrentAttackTaken = attackInfos;
            ModifyHP(-attackInfos.DamageAmount);
            EnemyEvents.Hit.Invoke(attackInfos);
        }
    }

    public virtual void ModifyHP(float value)
    {
        _currentHP += value;
    }
    public virtual void ApplyHeal(float amount)
    {
        ModifyHP(amount);
    }

    public virtual void OnDeath()
    {
        // Stuff to do on death
    }

    public void NullifyCurrentAttackTaken()
    {
        CurrentAttackTaken = null;
    }

    public void EnableAttack(EAttack_Types attack_type)
    {
        AttackBase attack = GetAttackType(attack_type);

        if (attack is null)
        {
            Debug.LogError("Attack not found");
            return;
        }

        attack.gameObject.SetActive(true);
        attack.EnableAttack(this);
    }

    public void DisableAttack(EAttack_Types attack_type)
    {
        AttackBase attack = GetAttackType(attack_type);

        if (attack is null)
        {
            Debug.LogError("Attack not found");
            return;
        }

        attack.DisableAttack();
    }

    private AttackBase GetAttackType(EAttack_Types attack_type)
    {
        AttackBase attack = null;

        switch (attack_type)
        {
            case EAttack_Types.MELEE:
                attack = MeleeAttack;
                break;
            case EAttack_Types.THROW:
                attack = ThrowAttack;
                break;
        }

        return attack;
    }
}
