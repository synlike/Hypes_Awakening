using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public static readonly GameEvent MeleeDone = new();
    public static readonly GameEvent MeleeCancellable = new();

    public void MeleeDoneEvent()
    {
        MeleeDone.Invoke();
    }
    public void MeleeCancellableEvent()
    {
        MeleeCancellable.Invoke();
    }
}
