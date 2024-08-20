using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerMoveState
{
    //private Vector2 currentMovementInput;
    private Vector3 oldMovement;
    private Vector3 currentMovementAnimation;
    private float rotationFactorPerFrame = 10.0f;
    private float smoothMoveElapsedTime = 0.0f;

    // Move Elsewhere (SO on player state machine with all infos ???)
    private float playerSpeed = 5.0f;
    private float smoothMoveTime = 0.5f;

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


        if (!Context.PlayerController.IsMovementPressed && currentMovementAnimation.x == 0f && currentMovementAnimation.z == 0f)
        {
            // Go To Idle State
            Debug.LogWarning("QUITTING RUN");
            NextState = PlayerStateMachine.EPlayerState.IDLE;
        }
        else
        {
            if (oldMovement != Context.PlayerController.CurrentMovement)
            {
                smoothMoveElapsedTime = 0.0f;
                oldMovement = Context.PlayerController.CurrentMovement;
            }

            HandleSmoothMove();
            HandleRotation();
        }
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
        if (smoothMoveElapsedTime < smoothMoveTime)
        {
            float ratio = Mathf.Clamp01(smoothMoveElapsedTime / smoothMoveTime);

            // Stop slerp if direction changes from positive to negative and vice versa ????
            //currentMovement = Vector3.Slerp(currentMovement, new Vector3(currentMovementInput.x, 0.0f, currentMovementInput.y), ratio); // Smooth Move cause issue with "U Turn"

            currentMovementAnimation = Vector3.Slerp(currentMovementAnimation, new Vector3(Mathf.Abs(Context.PlayerController.CurrentMovementInput.x), 0.0f, Mathf.Abs(Context.PlayerController.CurrentMovementInput.y)), ratio);

            smoothMoveElapsedTime += Time.deltaTime;
        }
        else
        {
            //currentMovement.x = currentMovementInput.x; // Issue with "U Turn"
            //currentMovement.z = currentMovementInput.y;

            currentMovementAnimation.x = Mathf.Abs(Context.PlayerController.CurrentMovementInput.x);
            currentMovementAnimation.z = Mathf.Abs(Context.PlayerController.CurrentMovementInput.y);
        }

        Context.PlayerAnimator.SetFloat("VelocityX", Mathf.Abs(currentMovementAnimation.x));
        Context.PlayerAnimator.SetFloat("VelocityY", Mathf.Abs(currentMovementAnimation.z));

        Context.CharacterController.Move(Context.PlayerController.CurrentMovement * playerSpeed * Time.deltaTime);
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
