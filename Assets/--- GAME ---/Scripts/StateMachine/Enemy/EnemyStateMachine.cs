using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EEnemyState>
{
    public EnemyBase Enemy { get; private set; }

    public enum EEnemyState
    {
        WANDER,
        AGGRESSIVE,
        HIT,
        DEATH,
        RESURRECT,
        ATTACK,
    }

    protected virtual void Awake()
    {
        Enemy = GetComponent<EnemyBase>();

        CurrentState = States[EEnemyState.WANDER];
    }
}
