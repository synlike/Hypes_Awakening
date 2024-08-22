using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{ 
    public CharacterController CharacterController {  get; private set; }
    public PlayerController PlayerController { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    public PlayerInput PlayerInput { get; private set; }


    public enum EPlayerState
    {
        IDLE,
        RUN,
        MELEE,
        BLOCK,
        DEATH,
    }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        PlayerController = GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerInput = new PlayerInput();

        States.Add(EPlayerState.IDLE, new PlayerIdleState(this, EPlayerState.IDLE));
        States.Add(EPlayerState.RUN, new PlayerRunState(this, EPlayerState.RUN));
        States.Add(EPlayerState.MELEE, new PlayerMeleeState(this, EPlayerState.MELEE));
        //States.Add(EPlayerState.DEATH, new PlayerDeathState(this, EPlayerState.DEATH));

        CurrentState = States[EPlayerState.IDLE];
    }

    private void OnEnable()
    {
        PlayerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.CharacterControls.Disable();
    }
}
