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
    private float smoothAnimationTransitionTime = 0.0f;
    private bool isLookingLeft;
    private bool previousIsBlocking;
    private bool previousIsMeleePressed;
    private bool isMeleePressed;

    private ECameraTargetPosition cameraTargetPosition = ECameraTargetPosition.UNSET;

    public PlayerRunState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Player entered Run State");
        PlayerEvents.BlockPressed.Add(OnBlockPressed);
        PlayerEvents.BlockReleased.Add(OnBlockReleased);
        PlayerEvents.MeleePressed.Add(OnPlayerMeleePressed);
        PlayerAnimationEvents.MeleeDone.Add(OnMeleeDone);

        smoothAnimationTransitionTime = Context.PlayerController.PlayerData.SmoothAnimationTime;

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
        PlayerAnimationEvents.MeleeDone.Remove(OnMeleeDone);
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

        if(previousMovement != currentMovement || previousIsBlocking != IsBlocking || previousIsMeleePressed != isMeleePressed)
        {
            if(previousIsBlocking != IsBlocking)
            {
                previousIsBlocking = IsBlocking;
            }

            if (previousIsMeleePressed != isMeleePressed)
            {
                if(isMeleePressed)
                {
                    smoothAnimationTransitionTime = Context.PlayerController.PlayerData.SmoothAnimationTimeMelee;
                }
                else
                {
                    smoothAnimationTransitionTime = Context.PlayerController.PlayerData.SmoothAnimationTime;
                }

                previousIsMeleePressed = isMeleePressed;
            }

            if (!Context.PlayerController.IsMovementPressed)
            {
                playerSpeed = 0f;
                DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 0f, smoothAnimationTransitionTime);
            }
            else if (Mathf.Abs(movement.x) < Context.PlayerController.PlayerData.RunAnimationTreshold && Mathf.Abs(movement.z) < Context.PlayerController.PlayerData.RunAnimationTreshold
                || IsBlocking || isMeleePressed)
            {
                playerSpeed = Context.PlayerController.PlayerData.WalkSpeed;
                DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 0.5f, smoothAnimationTransitionTime);
            }
            else
            {
                playerSpeed = Context.PlayerController.PlayerData.RunSpeed;
                DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 1.0f, smoothAnimationTransitionTime);
            }

            previousMovement = currentMovement;

            if (currentMovement.x > 0)
            {
                if(cameraTargetPosition != ECameraTargetPosition.LEFT)
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
        }

        Context.PlayerAnimator.SetFloat(AnimatorStateHashes.Velocity, playerAnimationVelocity);

        Context.CharacterController.Move(movement.normalized * playerSpeed * Time.deltaTime);

        if (isMeleePressed && playerAnimationVelocity == 0.5f)
        {
            NextState = PlayerStateMachine.EPlayerState.MELEE;
        }
    }

    private void OnPlayerMeleePressed()
    {
        isMeleePressed = true;
    }

    protected override void OnBlockPressed()
    {
        base.OnBlockPressed();
    }

    protected override void OnBlockReleased()
    {
        base.OnBlockReleased();
    }

    private void OnMeleeDone()
    {
        isMeleePressed = false;
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
