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

        PlayerEvents.BlockPressed.Add(OnBlockPressed);
        PlayerEvents.BlockReleased.Add(OnBlockReleased);
        PlayerEvents.MeleePressed.Add(OnPlayerMeleePressed);
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Player exited Idle State");

        PlayerEvents.BlockPressed.Remove(OnBlockPressed);
        PlayerEvents.BlockReleased.Remove(OnBlockReleased);
        PlayerEvents.MeleePressed.Remove(OnPlayerMeleePressed);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(Context.Player.Movement.IsMovementPressed)
        {
            NextState = PlayerStateMachine.EPlayerState.RUN;
        }
    }

    protected override void OnBlockPressed()
    {
        base.OnBlockPressed();
    }

    protected override void OnBlockReleased()
    {
        base.OnBlockReleased();
    }

    private void OnPlayerMeleePressed()
    {
        if (AllowActions && NextState != PlayerStateMachine.EPlayerState.MELEE) // second condition no useful ?
        {
            NextState = PlayerStateMachine.EPlayerState.MELEE;
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
