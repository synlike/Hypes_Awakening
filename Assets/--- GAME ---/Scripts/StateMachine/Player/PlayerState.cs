using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerStateMachine Context { get; private set; }

    protected PlayerStateMachine.EPlayerState NextState;

    public PlayerState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(key)
    {
        Context = context;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
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

    public override void UpdateState()
    {
    }
}
