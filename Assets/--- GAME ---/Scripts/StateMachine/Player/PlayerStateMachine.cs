using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState>
{
    public enum PlayerState
    {
        IDLE,
        RUN,
        ATTACK,
        DEATH,
    }

    private void Awake()
    {
        States.Add(PlayerState.IDLE, new PlayerIdleState(PlayerState.IDLE));
        States.Add(PlayerState.RUN, new PlayerIdleState(PlayerState.RUN));
        States.Add(PlayerState.ATTACK, new PlayerIdleState(PlayerState.ATTACK));
        States.Add(PlayerState.DEATH, new PlayerIdleState(PlayerState.DEATH));


        CurrentState = States[PlayerState.IDLE];
    }
}
