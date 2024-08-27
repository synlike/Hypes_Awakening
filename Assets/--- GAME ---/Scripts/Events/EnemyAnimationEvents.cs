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
}
