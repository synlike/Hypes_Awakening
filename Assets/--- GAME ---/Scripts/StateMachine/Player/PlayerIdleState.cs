using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState<PlayerStateMachine.PlayerState>
{
    public PlayerIdleState(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Player entered Idle State");
        PlayerManager.Instance.animator.SetTrigger("Idle");
    }

    public override void ExitState()
    {
        Debug.Log("Player exited Idle State");
    }

    public override void UpdateState()
    {
        Debug.Log("Player is in Idle State");
    }

    public override PlayerStateMachine.PlayerState GetNextState()
    {
        return PlayerManager.Instance.NextState;
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
