using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    AttackInfos CurrentAttackTaken { get;}

    void ApplyDamage(AttackInfos attackInfos);
    void ApplyHeal(float amount);
    void NullifyCurrentAttackTaken();

}
