using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static readonly GameEvent<ECameraTargetPosition> PlayerLookDirectionChanged = new();
}
