using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState<PlayerStateMachine.EPlayerState>
{
    // MAKE FILE WITH ALL CONST VARIABLES
    private const float LAYER_WEIGHT_ON = 1.0f;
    private const float LAYER_WEIGHT_OFF = 0.0f;
    private const int BLOCK_LAYER_ID = 1;

    public PlayerStateMachine Context { get; private set; }

    protected static PlayerStateMachine.EPlayerState NextState;

    protected static bool AllowActions = true;

    protected static bool IsBlocking = false;

    protected static bool IsMelee = false;


    // Block layer lerp
    protected static float StartBlockWeightValue = 0.0f;
    protected static float blockWeightLerpTimer = 0.0f;

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

    public override void UpdateState()
    {
        if (IsBlocking && Context.Player.Animator.GetLayerWeight(BLOCK_LAYER_ID) != LAYER_WEIGHT_ON)
        {
            // lerp layer weight to 1
            Context.Player.Animator.SetLayerWeight
                (BLOCK_LAYER_ID, Mathf.Lerp(StartBlockWeightValue, LAYER_WEIGHT_ON, blockWeightLerpTimer / Context.Player.Data.BlockWeightLerpDuration));

            blockWeightLerpTimer += Time.deltaTime;

            if(blockWeightLerpTimer >= Context.Player.Data.BlockWeightLerpDuration)
            {
                Context.Player.Animator.SetLayerWeight(BLOCK_LAYER_ID, LAYER_WEIGHT_ON);
            }
        }
        else if (!IsBlocking && Context.Player.Animator.GetLayerWeight(BLOCK_LAYER_ID) != LAYER_WEIGHT_OFF)
        {
            // lerp layer weight to 0
            Context.Player.Animator.SetLayerWeight
                (BLOCK_LAYER_ID, Mathf.Lerp(StartBlockWeightValue, LAYER_WEIGHT_OFF, blockWeightLerpTimer / Context.Player.Data.BlockWeightLerpDuration));

            blockWeightLerpTimer += Time.deltaTime;

            if (blockWeightLerpTimer >= Context.Player.Data.BlockWeightLerpDuration)
            {
                Context.Player.Animator.SetLayerWeight(BLOCK_LAYER_ID, LAYER_WEIGHT_OFF);
            }
        }
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        return NextState;
    }

    protected virtual void OnBlockPressed()
    {
        InitBlockLerp(true);
    }

    protected virtual void OnBlockReleased()
    {
        InitBlockLerp(false);
    }

    private void InitBlockLerp(bool isBlocking)
    {
        StartBlockWeightValue = Context.Player.Animator.GetLayerWeight(BLOCK_LAYER_ID);
        blockWeightLerpTimer = Context.Player.Data.BlockWeightLerpDuration - blockWeightLerpTimer;
        IsBlocking = isBlocking;
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
