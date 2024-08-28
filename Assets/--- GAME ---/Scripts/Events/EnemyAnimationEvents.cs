using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    public static readonly GameEvent HitDone = new();

    public void HitDoneEvent()
    {
        HitDone.Invoke();
    }

    public static readonly GameEvent ResurrectDone = new();

    public void ResurrectDoneEvent()
    {
        ResurrectDone.Invoke();
    }
}
