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

    public PlayerStateMachine.EPlayerState NextState { get; protected set; }

    // Block layer lerp
    public float StartBlockWeightValue { get; private set; } = 0.0f;
    public float BlockWeightLerpTimer { get; private set; } = 0.0f;

    public PlayerState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(key)
    {
        Context = context;
    }

    public override void EnterState()
    {
        InitBlockLerp();
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (Context.Player.Inputs.IsBlocking && Context.Player.Animator.GetLayerWeight(BLOCK_LAYER_ID) != LAYER_WEIGHT_ON)
        {
            // lerp layer weight to 1
            Context.Player.Animator.SetLayerWeight
                (BLOCK_LAYER_ID, Mathf.Lerp(StartBlockWeightValue, LAYER_WEIGHT_ON, BlockWeightLerpTimer / Context.Player.Data.BlockWeightLerpDuration));

            BlockWeightLerpTimer += Time.deltaTime;

            if(BlockWeightLerpTimer >= Context.Player.Data.BlockWeightLerpDuration)
            {
                Context.Player.Animator.SetLayerWeight(BLOCK_LAYER_ID, LAYER_WEIGHT_ON);
            }
        }
        else if (!Context.Player.Inputs.IsBlocking && Context.Player.Animator.GetLayerWeight(BLOCK_LAYER_ID) != LAYER_WEIGHT_OFF)
        {
            // lerp layer weight to 0
            Context.Player.Animator.SetLayerWeight
                (BLOCK_LAYER_ID, Mathf.Lerp(StartBlockWeightValue, LAYER_WEIGHT_OFF, BlockWeightLerpTimer / Context.Player.Data.BlockWeightLerpDuration));

            BlockWeightLerpTimer += Time.deltaTime;

            if (BlockWeightLerpTimer >= Context.Player.Data.BlockWeightLerpDuration)
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
        InitBlockLerp();
    }

    protected virtual void OnBlockReleased()
    {
        InitBlockLerp();
    }

    private void InitBlockLerp()
    {
        StartBlockWeightValue = Context.Player.Animator.GetLayerWeight(BLOCK_LAYER_ID);
        BlockWeightLerpTimer = Mathf.Clamp(Context.Player.Data.BlockWeightLerpDuration - BlockWeightLerpTimer, 0f, Mathf.Infinity);
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
