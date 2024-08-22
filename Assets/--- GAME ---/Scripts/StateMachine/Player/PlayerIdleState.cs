using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Player entered Idle State");
        NextState = PlayerStateMachine.EPlayerState.IDLE;
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Player exited Idle State");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(Context.PlayerController.IsMovementPressed)
        {
            NextState = PlayerStateMachine.EPlayerState.RUN;
        }
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
