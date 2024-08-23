using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    [field: SerializeField] public PlayerData PlayerData { get; private set; }

    [SerializeField] private bool enable8DirectionalInputs = true;
    public CameraTarget CameraTarget { get; private set; }
    public bool IsMovementPressed { get; private set; }
    public bool CanRun { get; private set; }
    public Vector2 CurrentMovementInput { get; private set; }
    public Vector2 CurrentRunInput { get; private set; }


    private void Awake()
    {
        playerInput = new PlayerInput();
        CameraTarget = GetComponent<CameraTarget>();

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

        playerInput.CharacterControls.Melee.started += context =>
        {
            OnMeleePressed(context);
        };

        playerInput.CharacterControls.Block.started += context =>
        {
            OnBlockPressed(context);
        };

        playerInput.CharacterControls.Block.canceled += context =>
        {
            OnBlockReleased(context);
        };
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();

        IsMovementPressed = movementInput.x != 0 || movementInput.y != 0;
        CanRun = Mathf.Abs(movementInput.x) >= PlayerData.WalkAnimationTreshold || Mathf.Abs(movementInput.y) >= PlayerData.WalkAnimationTreshold;

        if(enable8DirectionalInputs)
        {
            Calculate8DirMovement(movementInput);
        }
        else
        {
            CurrentMovementInput = movementInput;
        }
    }

    private void Calculate8DirMovement(Vector2 movementInput)
    {
        if(IsMovementPressed)
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            angle = Mathf.Round(angle / 45.0f) * 45.0f;

            float horizontalOut = Mathf.Round(Mathf.Cos(angle * Mathf.Deg2Rad));
            float verticalOut = Mathf.Round(Mathf.Sin(angle * Mathf.Deg2Rad));

            CurrentMovementInput = new Vector2(horizontalOut, verticalOut);

            CurrentMovementInput.Normalize();
        }
        else
        {
            CurrentMovementInput = Vector2.zero;
        }
    }

    private void OnMeleePressed(InputAction.CallbackContext context)
    {
        PlayerEvents.MeleePressed.Invoke();
    }

    private void OnBlockPressed(InputAction.CallbackContext context)
    {
        PlayerEvents.BlockPressed.Invoke();
    }

    private void OnBlockReleased(InputAction.CallbackContext context)
    {
        PlayerEvents.BlockReleased.Invoke();
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
    }

    private void OnDestroy()
    {
    }
}
