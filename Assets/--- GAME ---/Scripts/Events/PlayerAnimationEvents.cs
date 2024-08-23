using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public static readonly GameEvent MeleeDone = new();
    public static readonly GameEvent MeleeCancellable = new();

    public static readonly GameEvent BlockDone = new();

    public void MeleeDoneEvent()
    {
        MeleeDone.Invoke();
    }
    public void MeleeCancellableEvent()
    {
        MeleeCancellable.Invoke();
    }
    public void BlockDoneEvent()
    {
        BlockDone.Invoke();
    }
}
