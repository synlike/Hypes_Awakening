using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateEvents : MonoBehaviour
{
    public static readonly GameEvent OnRunStateEntered = new();
    public static readonly GameEvent OnRunStateExited = new();

    public static readonly GameEvent OnIdleStateEntered = new();
}
