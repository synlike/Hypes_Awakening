using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public static readonly GameEvent<ECameraTargetPosition> LookDirectionChanged = new();

    public static readonly GameEvent MeleePressed = new();
    public static readonly GameEvent BlockPressed = new();
    public static readonly GameEvent BlockReleased = new();

}
