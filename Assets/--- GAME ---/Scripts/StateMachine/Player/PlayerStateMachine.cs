using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{
    public PlayerBase Player { get; private set; }

    public enum EPlayerState
    {
        IDLE,
        RUN,
        MELEE,
        BLOCK,
        HIT,
        DEATH,
    }

    private void Awake()
    {
        Player = GetComponent<PlayerBase>();

        States.Add(EPlayerState.IDLE, new PlayerIdleState(this, EPlayerState.IDLE));
        States.Add(EPlayerState.RUN, new PlayerRunState(this, EPlayerState.RUN));
        States.Add(EPlayerState.MELEE, new PlayerMeleeState(this, EPlayerState.MELEE));
        //States.Add(EPlayerState.HIT, new PlayerHitState(this, EPlayerState.HIT));
        //States.Add(EPlayerState.DEATH, new PlayerDeathState(this, EPlayerState.DEATH));

        CurrentState = States[EPlayerState.IDLE];
    }
}
