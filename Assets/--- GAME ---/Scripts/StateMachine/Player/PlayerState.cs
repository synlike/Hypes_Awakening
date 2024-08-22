using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState<PlayerStateMachine.EPlayerState>
{
    public PlayerStateMachine Context { get; private set; }

    protected static PlayerStateMachine.EPlayerState NextState;

    protected static bool AllowActions = true;

    public PlayerState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(key)
    {
        Context = context;
    }

    public override void EnterState()
    {
        PlayerEvents.MeleePressed.Add(OnPlayerMeleePressed); // Maybe add something to prevent sub / unsub at every switch (only when switching states that cannot attack)
        PlayerEvents.BlockPressed.Add(OnBlockPressed);
        PlayerEvents.BlockReleased.Add(OnBlockReleased);
    }

    public override void ExitState()
    {
        PlayerEvents.MeleePressed.Remove(OnPlayerMeleePressed);
        PlayerEvents.BlockPressed.Remove(OnBlockPressed);
        PlayerEvents.BlockReleased.Remove(OnBlockReleased);
    }

    public override void UpdateState()
    {
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        return NextState;
    }

    private void OnPlayerMeleePressed()
    {
        if (AllowActions &&  NextState != PlayerStateMachine.EPlayerState.MELEE)
        {
            NextState = PlayerStateMachine.EPlayerState.MELEE;
        }
    }

    private void OnBlockPressed()
    {
        if (AllowActions && NextState != PlayerStateMachine.EPlayerState.BLOCK)
        {
            //NextState = PlayerStateMachine.EPlayerState.BLOCK;
        }
    }

    private void OnBlockReleased()
    {

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
