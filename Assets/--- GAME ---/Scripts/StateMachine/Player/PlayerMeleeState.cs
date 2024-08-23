using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : PlayerState
{
    private bool isMeleeCancellable = true;

    // CAN TRANSITIONED TO BLOCK STATE AT ANY TIME (NO NEED TO COMBINE ANIMATIONS HERE => BUT BLOCK CAN MELEE COMBINED)

    public PlayerMeleeState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Player entered Melee State");

        //NextState = PlayerStateMachine.EPlayerState.MELEE;

        PlayerEvents.MeleePressed.Add(OnPlayerMeleePressed); // Maybe add something to prevent sub / unsub at every switch (only when switching states that cannot attack)
        PlayerAnimationEvents.MeleeDone.Add(OnMeleeDone);
        PlayerAnimationEvents.MeleeCancellable.Add(OnMeleeCancellable);
        PlayerEvents.BlockPressed.Add(OnBlockPressed);
        PlayerEvents.BlockReleased.Add(OnBlockReleased);
        isMeleeCancellable = true;
        OnPlayerMeleePressed();
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Player exited Melee State");

        PlayerEvents.MeleePressed.Remove(OnPlayerMeleePressed);
        PlayerAnimationEvents.MeleeDone.Remove(OnMeleeDone);
        PlayerAnimationEvents.MeleeCancellable.Remove(OnMeleeCancellable);
        PlayerEvents.BlockPressed.Remove(OnBlockPressed);
        PlayerEvents.BlockReleased.Remove(OnBlockReleased);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void OnPlayerMeleePressed()
    {
        if (isMeleeCancellable)
        {
            isMeleeCancellable = false;
            Context.PlayerAnimator.SetTrigger(AnimatorStateHashes.Melee);
        }
    }

    private void OnMeleeCancellable()
    {
        isMeleeCancellable = true;
    }

    private void OnMeleeDone()
    {
        isMeleeCancellable = false;
        IsMelee = false;
        NextState = PlayerStateMachine.EPlayerState.IDLE;
    }

    protected override void OnBlockPressed()
    {
        base.OnBlockPressed();
    }

    protected override void OnBlockReleased()
    {
        base.OnBlockReleased();
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
