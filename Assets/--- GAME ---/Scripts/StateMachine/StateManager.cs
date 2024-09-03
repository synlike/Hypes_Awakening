using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{

    [ReadOnly]
    public EState CurrentStateDebug;

    public Dictionary<EState, BaseState<EState>> States { get; protected set; } = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool IsSwitchingState = false;

    void Start()
    {
        CurrentState.EnterState();
        CurrentStateDebug = CurrentState.StateKey;
    }

    void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();

        if(!IsSwitchingState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if (!IsSwitchingState)
        {
            SwitchState(nextStateKey);
            CurrentStateDebug = CurrentState.StateKey;
        }
    }

    public void SwitchState(EState stateKey)
    {
        IsSwitchingState = true;

        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();

        IsSwitchingState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}
