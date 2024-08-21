using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerRunState : PlayerMoveState
{
    //private Vector2 currentMovementInput;
    private Vector3 oldMovement;
    private Vector3 currentMovementAnimation;
    private float rotationFactorPerFrame = 10.0f;
    private float smoothMoveElapsedTime = 0.0f;
    private float smoothAnimationElapsedTime = 0.0f;

    // Move Elsewhere (SO on player state machine with all infos ???)
    private float playerSpeed = 0.0f;
    private float playerWalkSpeed = 3.0f;
    private float playerRunSpeed = 6.0f;
    private float playerAnimationVelocity = 0.0f;

    private float smoothAnimationTime = 0.2f;
    private float runAnimationTreshold = 0.5f;

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
            if(playerAnimationVelocity < 0.05f)
            {
                playerAnimationVelocity = 0.0f;
                Context.PlayerAnimator.SetFloat(AnimatorStateHashes.Velocity, 0.0f);
                NextState = PlayerStateMachine.EPlayerState.IDLE;
            }
        }

        HandleSmoothMove();
        HandleRotation();
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = Context.PlayerController.CurrentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = Context.PlayerController.CurrentMovement.z;

        Quaternion currentRotation = Context.transform.rotation; // get from elsewhere (playerManager or else)

        if (Context.PlayerController.IsMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            Context.transform.rotation = targetRotation; // Snap
            //transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime); // Smooth
        }
    }

    void HandleSmoothMove()
    {
        Vector3 movement = Context.PlayerController.CurrentMovement;

        if(!Context.PlayerController.IsMovementPressed)
        {
            playerSpeed = 0f;
            DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 0f, smoothAnimationTime);
        }
        else if(Mathf.Abs(movement.x) < runAnimationTreshold && Mathf.Abs(movement.z) < runAnimationTreshold)
        {
            playerSpeed = playerWalkSpeed;
            DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 0.5f, smoothAnimationTime);
        }
        else
        {
            playerSpeed = playerRunSpeed;
            DOTween.To(() => playerAnimationVelocity, x => playerAnimationVelocity = x, 1.0f, smoothAnimationTime);
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
