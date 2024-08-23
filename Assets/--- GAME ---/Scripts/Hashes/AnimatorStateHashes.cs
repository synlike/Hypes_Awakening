using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public static class AnimatorStateHashes
{
    public static readonly int Velocity = Animator.StringToHash(nameof(Velocity));
    public static readonly int Melee = Animator.StringToHash(nameof(Melee));
}
