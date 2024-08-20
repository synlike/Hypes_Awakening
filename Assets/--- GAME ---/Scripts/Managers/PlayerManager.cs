using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Animator animator;

    [SerializeField] private PlayerStateMachine playerStateMachine;

    public PlayerStateMachine.EPlayerState NextState { get; private set; } = PlayerStateMachine.EPlayerState.IDLE;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    //public void Idle()
    //{
    //    NextState = PlayerStateMachine.EPlayerState.IDLE;
    //}

    //public void Run()
    //{
    //    NextState = PlayerStateMachine.EPlayerState.RUN;
    //}

    //public void Attack()
    //{
    //    NextState = PlayerStateMachine.EPlayerState.ATTACK;
    //}

    //public void Death()
    //{
    //    NextState = PlayerStateMachine.EPlayerState.DEATH;
    //}
}
