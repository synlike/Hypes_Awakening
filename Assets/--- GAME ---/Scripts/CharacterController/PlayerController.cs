using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator playerAnimator;

    private PlayerInput playerInput;
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 oldMovement;
    private Vector3 currentMovementAnimation;
    private float rotationFactorPerFrame = 10.0f;
    private float smoothMoveElapsedTime = 0.0f;

    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float smoothMoveTime = 0.5f;

    public bool IsMovementPressed {  get; private set; }


    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();

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
        currentMovementInput = context.ReadValue<Vector2>();

        if(currentMovement != new Vector3(currentMovementInput.x, 0.0f, currentMovementInput.y))
        {
            smoothMoveElapsedTime = 0.0f;
        }

        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        IsMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
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
        HandleSmoothMove();
        HandleRotation();
    }

    private void OnDestroy()
    {
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (IsMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = targetRotation; // Snap
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

            currentMovementAnimation = Vector3.Slerp(currentMovementAnimation, new Vector3(Mathf.Abs(currentMovementInput.x), 0.0f, Mathf.Abs(currentMovementInput.y)), ratio);

            smoothMoveElapsedTime += Time.deltaTime;
        }
        else
        {
            //currentMovement.x = currentMovementInput.x; // Issue with "U Turn"
            //currentMovement.z = currentMovementInput.y;

            currentMovementAnimation.x = Mathf.Abs(currentMovementInput.x);
            currentMovementAnimation.z = Mathf.Abs(currentMovementInput.y);
        }

        playerAnimator.SetFloat("VelocityX", Mathf.Abs(currentMovementAnimation.x));
        playerAnimator.SetFloat("VelocityY", Mathf.Abs(currentMovementAnimation.z));

        characterController.Move(currentMovement * playerSpeed * Time.deltaTime);
    }
}
