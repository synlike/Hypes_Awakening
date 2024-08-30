using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerRunState : PlayerState
{
    private Vector3 currentMovement;
    private Vector3 previousMovement;
    private float playerSpeed = 0.0f;
    private float playerAnimationVelocity = 0.0f;
    private float smoothAnimationTransitionDuration = 0.0f;
    private bool isLookingLeft;
    private bool previousIsBlocking;
    private bool previousIsMeleePressed;
    private bool previousCanRun;

    // Idle Walk Run lerp
    private float startBlendValue = 0.0f;
    private float goalBlendValue = 0.0f;
    private float blendTimer = 0.0f;

    private ECameraTargetPosition cameraTargetPosition = ECameraTargetPosition.UNSET;

    public PlayerRunState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        NextState = PlayerStateMachine.EPlayerState.RUN;

        //Debug.Log("Player entered Run State");
        PlayerEvents.BlockPressed.Add(OnBlockPressed);
        PlayerEvents.BlockReleased.Add(OnBlockReleased);

        smoothAnimationTransitionDuration = Context.Player.Data.SmoothAnimationDuration;
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Player exited Run State");

        playerSpeed = 0.0f;
        playerAnimationVelocity = 0.0f;
        currentMovement = Vector3.zero;
        previousMovement = Vector3.zero;
        Context.Player.Animator.SetFloat(AnimatorStateHashes.Velocity, playerAnimationVelocity);

        PlayerEvents.BlockPressed.Remove(OnBlockPressed);
        PlayerEvents.BlockReleased.Remove(OnBlockReleased);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!Context.Player.Inputs.IsMovementPressed)
        {
            if (playerAnimationVelocity < 0.05f)
            {
                playerAnimationVelocity = 0.0f;
                Context.Player.Animator.SetFloat(AnimatorStateHashes.Velocity, 0.0f);
                NextState = PlayerStateMachine.EPlayerState.IDLE;
            }
        }

        currentMovement = new Vector3(Context.Player.Inputs.CurrentMovementInput.x, 0.0f, Context.Player.Inputs.CurrentMovementInput.y);

        HandleMovement();
        HandleRotation();
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        if (Context.Player.Inputs.IsMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            Context.transform.rotation = targetRotation; // Snap
        }
    }

    void HandleMovement()
    {
        Vector3 movement = currentMovement;

        bool canRun = Context.Player.Inputs.CanRun;

        if (previousMovement != currentMovement || previousIsMeleePressed != Context.Player.Inputs.IsMelee || previousIsBlocking != Context.Player.Inputs.IsBlocking || previousCanRun != canRun)
        {
            smoothAnimationTransitionDuration = Context.Player.Data.SmoothAnimationDuration;
            previousMovement = currentMovement;
            previousIsMeleePressed = Context.Player.Inputs.IsMelee;
            previousIsBlocking = Context.Player.Inputs.IsBlocking;
            previousCanRun = canRun;

            if (Context.Player.Inputs.IsMelee)
            {
                //Debug.LogWarning("SETTING STOP MELEE");
                playerSpeed = Context.Player.Data.IdleSpeed;
                goalBlendValue = Context.Player.Data.IdleAnimationTreshold;
                smoothAnimationTransitionDuration = Context.Player.Data.SmoothAnimationDurationMelee;
                startBlendValue = playerAnimationVelocity;
            }
            else if (!Context.Player.Inputs.IsMovementPressed)
            {
                //Debug.LogWarning("SETTING STOP");
                playerSpeed = Context.Player.Data.IdleSpeed;
                goalBlendValue = Context.Player.Data.IdleAnimationTreshold;
                startBlendValue = playerAnimationVelocity;
            }
            else if (!canRun || Context.Player.Inputs.IsBlocking)
            {
                //Debug.LogWarning("SETTING WALK");
                playerSpeed = Context.Player.Data.WalkSpeed;
                goalBlendValue = Context.Player.Data.WalkAnimationTreshold;
                startBlendValue = playerAnimationVelocity;
            }
            else
            {
                //Debug.LogWarning("SETTING RUN");
                playerSpeed = Context.Player.Data.RunSpeed;
                goalBlendValue = Context.Player.Data.RunAnimationTreshold;
                startBlendValue = Mathf.Clamp(playerAnimationVelocity, Context.Player.Data.WalkAnimationTreshold, Context.Player.Data.RunAnimationTreshold);
            }

            blendTimer = 0.0f;
        }

        if (playerAnimationVelocity != goalBlendValue)
        {
            playerAnimationVelocity = Mathf.Lerp(startBlendValue, goalBlendValue, blendTimer / smoothAnimationTransitionDuration);

            blendTimer += Time.deltaTime;

            if (blendTimer >= smoothAnimationTransitionDuration)
            {
                playerAnimationVelocity = goalBlendValue;
            }

            Context.Player.Animator.SetFloat(AnimatorStateHashes.Velocity, playerAnimationVelocity);
        }

        if (currentMovement.x > 0)
        {
            if (cameraTargetPosition != ECameraTargetPosition.LEFT)
            {
                cameraTargetPosition = ECameraTargetPosition.LEFT;
                PlayerEvents.LookDirectionChanged.Invoke(cameraTargetPosition);
            }
        }
        else if (currentMovement.x < 0)
        {
            if (cameraTargetPosition != ECameraTargetPosition.RIGHT)
            {
                cameraTargetPosition = ECameraTargetPosition.RIGHT;
                PlayerEvents.LookDirectionChanged.Invoke(cameraTargetPosition);
            }
        }

        Context.Player.CharacterController.Move(movement.normalized * playerSpeed * Time.deltaTime);

        if (Context.Player.Inputs.IsMelee && playerAnimationVelocity == Context.Player.Data.IdleAnimationTreshold)
        {
            NextState = PlayerStateMachine.EPlayerState.MELEE;
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
