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
        if (IsBlocking && Context.PlayerAnimator.GetLayerWeight(BLOCK_LAYER_ID) != LAYER_WEIGHT_ON)
        {
            Debug.LogWarning("LERPING TO 1");
            // lerp layer weight to 1
            Context.PlayerAnimator.SetLayerWeight
                (BLOCK_LAYER_ID, Mathf.Lerp(StartBlockWeightValue, 1.0f, blockWeightLerpTimer / Context.PlayerController.PlayerData.TimeToBlockWeightLerp));

            blockWeightLerpTimer += Time.deltaTime;

            if(blockWeightLerpTimer >= Context.PlayerController.PlayerData.TimeToBlockWeightLerp)
            {
                Context.PlayerAnimator.SetLayerWeight(BLOCK_LAYER_ID, LAYER_WEIGHT_ON);
            }
        }
        else if (!IsBlocking && Context.PlayerAnimator.GetLayerWeight(BLOCK_LAYER_ID) != LAYER_WEIGHT_OFF)
        {
            Debug.LogWarning("LERPING TO 0");
            // lerp layer weight to 0
            Context.PlayerAnimator.SetLayerWeight
                (BLOCK_LAYER_ID, Mathf.Lerp(StartBlockWeightValue, 0.0f, blockWeightLerpTimer / Context.PlayerController.PlayerData.TimeToBlockWeightLerp));

            blockWeightLerpTimer += Time.deltaTime;

            if (blockWeightLerpTimer >= Context.PlayerController.PlayerData.TimeToBlockWeightLerp)
            {
                Context.PlayerAnimator.SetLayerWeight(BLOCK_LAYER_ID, LAYER_WEIGHT_OFF);
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
        StartBlockWeightValue = Context.PlayerAnimator.GetLayerWeight(BLOCK_LAYER_ID);
        blockWeightLerpTimer = Context.PlayerController.PlayerData.TimeToBlockWeightLerp - blockWeightLerpTimer;
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
