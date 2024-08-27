using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class EnemyEvents
{
    public static readonly GameEvent<AttackInfos> Hit = new();
    public static readonly GameEvent HitDone = new();
}
