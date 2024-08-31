using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : AttackBase
{
    private EntityBase _origin;

    Vector3 direction;

    private bool isEnabled = false;

    private void Update()
    {
        if(isEnabled)
        {
            transform.position += direction * Data.ProjectileSpeed * Time.deltaTime;
        }
    }

    public override void EnableAttack(EntityBase origin)
    {
        direction = origin.transform.forward;
        _origin = origin;
        isEnabled = true;
    }

    public override void DisableAttack() 
    { 
        isEnabled = false;
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        DisableAttack();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);


        // Also Destroy when hitting walls

        if (other.TryGetComponent(out IDamageable hit) && hit != _origin as IDamageable) // AND HIT IS NOT SELF
        {
            if(_origin == null)
            {
                Debug.LogError("Origin is null");
                return;
            }

            hit.ApplyDamage(new AttackInfos(hit, _origin.transform, Data.Damage, Data.KnockbackAmount));

            DisableAttack();
        }
        else if(hit != _origin as IDamageable)
        {
            DisableAttack();
        }
    }
}
