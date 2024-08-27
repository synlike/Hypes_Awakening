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
    }

    private void Awake()
    {
        Enemy = GetComponent<EnemyBase>();

        States.Add(EEnemyState.WANDER, new EnemyWanderState(this, EEnemyState.WANDER));
        States.Add(EEnemyState.HIT, new EnemyHitState(this, EEnemyState.HIT));
        //States.Add(EEnemyState.AGGRESSIVE, new PlayerRunState(this, EEnemyState.AGGRESSIVE));
        //States.Add(EEnemyState.DEATH, new PlayerMeleeState(this, EEnemyState.DEATH));

        CurrentState = States[EEnemyState.WANDER];
    }
}
