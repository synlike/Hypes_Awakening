using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : PlayerState
{
    private float playerSpeed = 0.0f;
    private Vector3 currentMovement;
    private bool isMeleeCancellable = true;

    // CAN TRANSITIONED TO BLOCK STATE AT ANY TIME (NO NEED TO COMBINE ANIMATIONS HERE => BUT BLOCK CAN MELEE COMBINED)

    public PlayerMeleeState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Player entered Melee State");

        //NextState = PlayerStateMachine.EPlayerState.MELEE;

        PlayerEvents.MeleePressed.Add(OnPlayerMeleePressed); // Maybe add something to prevent sub / unsub at every switch (only when switching states that cannot attack)
        PlayerAnimationEvents.MeleeDone.Add(OnMeleeDone);
        PlayerAnimationEvents.MeleeCancellable.Add(OnMeleeCancellable);
        isMeleeCancellable = true;
        OnPlayerMeleePressed();
    }

    private void OnPlayerMeleePressed()
    {
        if(isMeleeCancellable)
        {
            isMeleeCancellable = false;
            Context.PlayerAnimator.SetTrigger(AnimatorStateHashes.Melee);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Player exited Melee State");

        PlayerEvents.MeleePressed.Remove(OnPlayerMeleePressed);
        PlayerAnimationEvents.MeleeDone.Remove(OnMeleeDone);
        PlayerAnimationEvents.MeleeCancellable.Remove(OnMeleeCancellable);
    }

    public override void UpdateState()
    {
    }

    private void OnMeleeCancellable()
    {
        isMeleeCancellable = true;
    }

    private void OnMeleeDone()
    {
        isMeleeCancellable = false;
        NextState = PlayerStateMachine.EPlayerState.IDLE;
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
