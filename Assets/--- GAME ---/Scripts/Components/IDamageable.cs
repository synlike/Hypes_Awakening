using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(int amount);
    void ApplyHeal(int amount);
}
