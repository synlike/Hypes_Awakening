using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    // Player Speed Lerp
    private float timeElapsed;

    public PlayerRunState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Player entered Run State");
        NextState = PlayerStateMachine.EPlayerState.RUN;

        PlayerStateEvents.OnRunStateEntered?.Invoke();
    }

    public override void ExitState()
    {
        Debug.Log("Player exited Run State");

        PlayerStateEvents.OnRunStateExited?.Invoke();
    }

    public override void UpdateState()
    {
        Debug.Log("Player is in Run State");

        if (!Context.PlayerController.IsMovementPressed)
        {
            // Go To Idle State
            NextState = PlayerStateMachine.EPlayerState.IDLE;
        }
        else
        {
            // Handle run anim and behaviour
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
