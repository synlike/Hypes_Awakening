using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Animator animator;

    [SerializeField] private PlayerStateMachine playerStateMachine;

    public PlayerStateMachine.PlayerState NextState { get; private set; } = PlayerStateMachine.PlayerState.IDLE;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void Idle()
    {
        NextState = PlayerStateMachine.PlayerState.IDLE;
    }

    public void Run()
    {
        NextState = PlayerStateMachine.PlayerState.RUN;
    }

    public void Attack()
    {
        NextState = PlayerStateMachine.PlayerState.ATTACK;
    }

    public void Death()
    {
        NextState = PlayerStateMachine.PlayerState.DEATH;
    }
}
