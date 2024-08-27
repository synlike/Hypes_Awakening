using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour, IDamageable, IHealth
{
    private int _currentHP;
    private int _maxHP;


    public int CurrentHP { get => _currentHP; set => _currentHP = value; }
    public int MaxHP { get => _maxHP; set => _maxHP = value; }


    public virtual void ApplyDamage(int amount)
    {
        ModifyHP(-amount);
    }

    public virtual void ModifyHP(int value)
    {
        _currentHP += value;
    }
    public virtual void ApplyHeal(int amount)
    {
        ModifyHP(amount);
    }

    public virtual void OnDie()
    {
        // Stuff to do on death
    }
}
