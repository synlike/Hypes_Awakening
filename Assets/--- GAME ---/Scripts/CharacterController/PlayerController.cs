using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Debug
    public float currentPlayerSpeed;

    private PlayerInput playerInput;
    private float rotationFactorPerFrame = 10.0f;
    private float smoothMoveElapsedTime = 0.0f;

    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float smoothMoveTime = 0.5f;

    public bool IsMovementPressed { get; private set; }
    public Vector2 CurrentMovementInput { get; private set; }
    public Vector3 CurrentMovement { get; private set; }
    //public Vector3 CurrentMovementAnimation { get; private set; }


    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Move.started += context =>
        {
            OnMovementInput(context);
        };

        playerInput.CharacterControls.Move.canceled += context =>
        {
            OnMovementInput(context);
        };

        playerInput.CharacterControls.Move.performed += context =>
        {
            OnMovementInput(context);
        };
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        CurrentMovementInput = context.ReadValue<Vector2>();

        //if(CurrentMovement != new Vector3(CurrentMovementInput.x, 0.0f, CurrentMovementInput.y))
        //{
        //    smoothMoveElapsedTime = 0.0f;
        //}

        CurrentMovement = new Vector3(CurrentMovementInput.x, 0.0f, CurrentMovementInput.y); // can be moved to PlayerRunState
        IsMovementPressed = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
    }


    void Start()
    {
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

    void Update()
    {
        //HandleSmoothMove();
        //HandleRotation();
    }

    private void OnDestroy()
    {
    }

    //void HandleRotation()
    //{
    //    Vector3 positionToLookAt;

    //    positionToLookAt.x = CurrentMovement.x;
    //    positionToLookAt.y = 0.0f;
    //    positionToLookAt.z = CurrentMovement.z;

    //    Quaternion currentRotation = transform.rotation;

    //    if (IsMovementPressed)
    //    {
    //        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
    //        transform.rotation = targetRotation; // Snap
    //        //transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime); // Smooth
    //    }
    //}

    //void HandleSmoothMove()
    //{
    //    if (smoothMoveElapsedTime < smoothMoveTime)
    //    {
    //        float ratio = Mathf.Clamp01(smoothMoveElapsedTime / smoothMoveTime);

    //        // Stop slerp if direction changes from positive to negative and vice versa ????
    //        //currentMovement = Vector3.Slerp(currentMovement, new Vector3(currentMovementInput.x, 0.0f, currentMovementInput.y), ratio); // Smooth Move cause issue with "U Turn"

    //        CurrentMovementAnimation = Vector3.Slerp(CurrentMovementAnimation, new Vector3(Mathf.Abs(CurrentMovementInput.x), 0.0f, Mathf.Abs(CurrentMovementInput.y)), ratio);

    //        smoothMoveElapsedTime += Time.deltaTime;
    //    }
    //    else
    //    {
    //        //currentMovement.x = currentMovementInput.x; // Issue with "U Turn"
    //        //currentMovement.z = currentMovementInput.y;

    //        CurrentMovementAnimation = new Vector3(Mathf.Abs(CurrentMovementInput.x), 0.0f, Mathf.Abs(CurrentMovementInput.y));
    //        //currentMovementAnimation.x = Mathf.Abs(CurrentMovementInput.x);
    //        //currentMovementAnimation.z = Mathf.Abs(CurrentMovementInput.y);
    //    }

    //    playerAnimator.SetFloat("VelocityX", Mathf.Abs(CurrentMovementAnimation.x));
    //    playerAnimator.SetFloat("VelocityY", Mathf.Abs(CurrentMovementAnimation.z));

    //    characterController.Move(CurrentMovement * playerSpeed * Time.deltaTime);
    //}
}
