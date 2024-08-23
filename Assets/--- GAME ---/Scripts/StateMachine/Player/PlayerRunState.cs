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

        //Debug.Log("Player entered Run State");
        PlayerEvents.BlockPressed.Add(OnBlockPressed);
        PlayerEvents.BlockReleased.Add(OnBlockReleased);
        PlayerEvents.MeleePressed.Add(OnPlayerMeleePressed);

        smoothAnimationTransitionDuration = Context.PlayerController.PlayerData.SmoothAnimationTime;

        NextState = PlayerStateMachine.EPlayerState.RUN;
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Player exited Run State");

        playerSpeed = 0.0f;
        playerAnimationVelocity = 0.0f;
        currentMovement = Vector3.zero;
        previousMovement = Vector3.zero;
        Context.PlayerAnimator.SetFloat(AnimatorStateHashes.Velocity, playerAnimationVelocity);

        PlayerEvents.BlockPressed.Remove(OnBlockPressed);
        PlayerEvents.BlockReleased.Remove(OnBlockReleased);
        PlayerEvents.MeleePressed.Remove(OnPlayerMeleePressed);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!Context.PlayerController.IsMovementPressed)
        {
            if (playerAnimationVelocity < 0.05f)
            {
                playerAnimationVelocity = 0.0f;
                Context.PlayerAnimator.SetFloat(AnimatorStateHashes.Velocity, 0.0f);
                NextState = PlayerStateMachine.EPlayerState.IDLE;
            }
        }

        currentMovement = new Vector3(Context.PlayerController.CurrentMovementInput.x, 0.0f, Context.PlayerController.CurrentMovementInput.y);

        HandleMovement();
        HandleRotation();
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        if (Context.PlayerController.IsMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            Context.transform.rotation = targetRotation; // Snap
        }
    }

    void HandleMovement()
    {
        Vector3 movement = currentMovement;

        bool canRun = Context.PlayerController.CanRun;

        if (previousMovement != currentMovement || previousIsMeleePressed != IsMelee || previousIsBlocking != IsBlocking || previousCanRun != canRun)
        {
            smoothAnimationTransitionDuration = Context.PlayerController.PlayerData.SmoothAnimationTime;
            previousMovement = currentMovement;
            previousIsMeleePressed = IsMelee;
            previousIsBlocking = IsBlocking;
            previousCanRun = canRun;

            if (IsMelee)
            {
                Debug.LogWarning("SETTING STOP MELEE");
                playerSpeed = Context.PlayerController.PlayerData.IdleSpeed;
                goalBlendValue = Context.PlayerController.PlayerData.IdleAnimationTreshold;
                smoothAnimationTransitionDuration = Context.PlayerController.PlayerData.SmoothAnimationTimeMelee;
                startBlendValue = playerAnimationVelocity;
            }
            else if (!Context.PlayerController.IsMovementPressed)
            {
                Debug.LogWarning("SETTING STOP");
                playerSpeed = Context.PlayerController.PlayerData.IdleSpeed;
                goalBlendValue = Context.PlayerController.PlayerData.IdleAnimationTreshold;
                startBlendValue = playerAnimationVelocity;
            }
            else if (!canRun || IsBlocking)
            {
                Debug.LogWarning("SETTING WALK");
                playerSpeed = Context.PlayerController.PlayerData.WalkSpeed;
                goalBlendValue = Context.PlayerController.PlayerData.WalkAnimationTreshold;
                startBlendValue = playerAnimationVelocity;
            }
            else
            {
                Debug.LogWarning("SETTING RUN");
                playerSpeed = Context.PlayerController.PlayerData.RunSpeed;
                goalBlendValue = Context.PlayerController.PlayerData.RunAnimationTreshold;
                startBlendValue = Mathf.Clamp(playerAnimationVelocity, Context.PlayerController.PlayerData.WalkAnimationTreshold, Context.PlayerController.PlayerData.RunAnimationTreshold);
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

            Context.PlayerAnimator.SetFloat(AnimatorStateHashes.Velocity, playerAnimationVelocity);
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

        Context.CharacterController.Move(movement.normalized * playerSpeed * Time.deltaTime);

        if (IsMelee && playerAnimationVelocity == Context.PlayerController.PlayerData.IdleAnimationTreshold)
        {
            NextState = PlayerStateMachine.EPlayerState.MELEE;
        }
    }

    private void OnPlayerMeleePressed()
    {
        IsMelee = true;
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
