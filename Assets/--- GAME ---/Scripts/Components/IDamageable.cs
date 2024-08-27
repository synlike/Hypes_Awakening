using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(AttackInfos attackInfos);
    void ApplyHeal(int amount);
}
