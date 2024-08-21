using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerMoveState
{
    public PlayerIdleState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Player entered Idle State");
        NextState = PlayerStateMachine.EPlayerState.IDLE;
    }

    public override void ExitState()
    {
        Debug.Log("Player exited Idle State");
    }

    public override void UpdateState()
    {
        Debug.Log("Player is in Idle State");

        if(Context.PlayerController.IsMovementPressed)
        {
            NextState = PlayerStateMachine.EPlayerState.RUN;
        }
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        return base.GetNextState();
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
