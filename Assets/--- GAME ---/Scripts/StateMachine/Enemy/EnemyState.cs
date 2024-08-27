using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyState : BaseState<EnemyStateMachine.EEnemyState>
{
    public EnemyStateMachine Context { get; private set; }

    public static AttackInfos CurrentAttackTaken { get; protected set; }

    protected static EnemyStateMachine.EEnemyState NextState;

    public EnemyState(EnemyStateMachine context, EnemyStateMachine.EEnemyState key) : base(key)
    {
        Context = context;
    }

    public override void EnterState()
    {
        EnemyEvents.Hit.Add(OnHit);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        EnemyEvents.Hit.Remove(OnHit);
    }

    private void OnHit(AttackInfos attackInfos)
    {
        if (ReferenceEquals(attackInfos.Damageable, Context.Enemy))
        {
            CurrentAttackTaken = attackInfos;
            NextState = EnemyStateMachine.EEnemyState.HIT;
        }
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        return NextState;
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    public override void OnTriggerStay(Collider other)
    {
    }
}
