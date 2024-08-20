using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
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

        if (Context.PlayerController.IsMovementPressed)
        {
            // Go To Run State
            NextState = PlayerStateMachine.EPlayerState.RUN;
        }
        else
        {
            // Handle idle anim and behaviour
            Context.PlayerAnimator.SetTrigger("Idle");
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
