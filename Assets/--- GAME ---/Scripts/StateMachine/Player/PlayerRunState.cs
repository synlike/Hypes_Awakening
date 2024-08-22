using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerRunState : PlayerState
{
    private Vector3 currentMovement;
    private float playerSpeed = 0.0f;
    private float playerAnimationVelocity = 0.0f;
    private bool isLookingLeft;

    private ECameraTargetPosition cameraTargetPosition = ECameraTargetPosition.UNSET;

    public PlayerRunState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Player entered Run State");
        NextState = PlayerStateMachine.EPlayerState.RUN;
    }

    public override void ExitState()
    {
        Debug.Log("Player exited Run State");
    }

    public override void UpdateState()
    {
        Debug.Log("Player is in Run State");


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

        if(!Context.PlayerController.IsMovementPressed)
        {
            playerSpeed = 0f;
            DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 0f, Context.PlayerController.PlayerData.SmoothAnimationTime);
        }
        else if(Mathf.Abs(movement.x) < Context.PlayerController.PlayerData.RunAnimationTreshold && Mathf.Abs(movement.z) < Context.PlayerController.PlayerData.RunAnimationTreshold)
        {
            playerSpeed = Context.PlayerController.PlayerData.WalkSpeed;
            DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 0.5f, Context.PlayerController.PlayerData.SmoothAnimationTime);
        }
        else
        {
            playerSpeed = Context.PlayerController.PlayerData.RunSpeed;
            DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 1.0f, Context.PlayerController.PlayerData.SmoothAnimationTime);
        }

        if (currentMovement.x > 0)
        {
            if(cameraTargetPosition != ECameraTargetPosition.LEFT)
            {
                cameraTargetPosition = ECameraTargetPosition.LEFT;
                PlayerEvents.PlayerLookDirectionChanged.Invoke(cameraTargetPosition);
            }
        }
        else if (currentMovement.x < 0)
        {
            if (cameraTargetPosition != ECameraTargetPosition.RIGHT)
            {
                cameraTargetPosition = ECameraTargetPosition.RIGHT;
                PlayerEvents.PlayerLookDirectionChanged.Invoke(cameraTargetPosition);
            }
        }

        Context.PlayerAnimator.SetFloat(AnimatorStateHashes.Velocity, playerAnimationVelocity);

        Context.CharacterController.Move(movement.normalized * playerSpeed * Time.deltaTime);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        return base.GetNextState();
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
