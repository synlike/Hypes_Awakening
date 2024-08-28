using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    int CurrentHP { get; set; }
    int MaxHP { get; set; }
    void ModifyHP(int value);
    void OnDeath();
}
