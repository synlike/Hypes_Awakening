using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour, IDamageable, IHealth
{
    private int _currentHP;
    private int _maxHP;
    private AttackInfos _currentAttackTaken;

    public int CurrentHP { get => _currentHP; set => _currentHP = value; }
    public int MaxHP { get => _maxHP; set => _maxHP = value; }
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

    public virtual void ModifyHP(int value)
    {
        _currentHP += value;
    }
    public virtual void ApplyHeal(int amount)
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
}
