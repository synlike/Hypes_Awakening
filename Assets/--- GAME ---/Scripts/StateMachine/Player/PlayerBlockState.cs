using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
{
    // PLAYER CAN :
    // BLOCK MELEE (Other State / Anim like the regular melee)
    // BLOCK RUN (Manage Here - Need animations combining)


    /* 
     * For the people that only sometimes want to affect the arms (or another part of the armature), 
     * you can have one 'full' mask and one mask that only controls the arms. And change the layer weight of
     * your arms layer to 1 whenever you want to use the arms for a certain animation and then change the weight 
     * to 0 when you want to use the full armature instead for an animation. You can change the weight through code of course.
    */

    public PlayerBlockState(PlayerStateMachine context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Player entered Block State");
    }

    public override void ExitState()
    {
        Debug.Log("Player exited Block State");
    }

    public override void UpdateState()
    {
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
