using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    [field: SerializeField] public AttackData Data { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable hit))
        {
            hit.ApplyDamage(Data.Damage);
            Debug.LogWarning(other.gameObject.name + " got hit");
        }
    }
}
