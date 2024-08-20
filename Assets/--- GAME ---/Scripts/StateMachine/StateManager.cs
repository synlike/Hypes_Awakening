using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    public Dictionary<EState, BaseState<EState>> States { get; protected set; } = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool IsSwitchingState = false;

    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();
        Debug.LogWarning("NEXT STATE : " + States[nextStateKey].ToString());

        if(!IsSwitchingState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if (!IsSwitchingState)
        {
            SwitchState(nextStateKey);
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
