using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttack_Types
{ 
    MELEE,
}

public class PlayerBase : EntityBase
{
    [field: SerializeField] public PlayerData Data { get; private set; }
    [field: SerializeField] public AttackBase MeleeAttack { get; private set; }

    public CharacterController CharacterController { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public Animator Animator { get; private set; }

    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Movement = GetComponent<PlayerMovement>();
        Animator = GetComponent<Animator>();

        MaxHP = Data.MaxHealth;
        CurrentHP = MaxHP;

        //PlayerEvents.MeleeStart.Add(OnMeleeStart);
    }

    private void OnDestroy()
    {
        //PlayerEvents.MeleeStart.Remove(OnMeleeStart);
    }

    void Update()
    {

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
    }

    public void DisableAttack(EAttack_Types attack_type)
    {
        AttackBase attack = GetAttackType(attack_type);

        if (attack is null)
        {
            Debug.LogError("Attack not found");
            return;
        }

        attack.gameObject.SetActive(false);
    }

    private AttackBase GetAttackType(EAttack_Types attack_type)
    {
        AttackBase attack = null;

        switch (attack_type)
        {
            case EAttack_Types.MELEE:
                attack = MeleeAttack;
                break;
        }

        return attack;
    }
}
