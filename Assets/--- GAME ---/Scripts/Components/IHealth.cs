using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float CurrentHP { get; set; }
    float MaxHP { get; set; }
    void ModifyHP(float value);
    void OnDeath();
}
