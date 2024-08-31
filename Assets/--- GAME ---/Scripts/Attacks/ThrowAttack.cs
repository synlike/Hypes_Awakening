using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttack : AttackBase
{
    // ProjectileAttack is AttackBase but also disable when touching walls

    private List<GameObject> pooledProjectiles;

    [SerializeField] private GameObject projectileInHand;
    [SerializeField] private GameObject projectileToPool;
    [SerializeField] private GameObject projectileReference;
    [SerializeField] private int amountToPool = 3;


    private void Start()
    {
        pooledProjectiles = new List<GameObject>();

        GameObject tmp;

        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(projectileToPool);
            tmp.SetActive(false);
            pooledProjectiles.Add(tmp);
        }

        gameObject.SetActive(false);
    }

    public override void EnableAttack(EntityBase origin)
    {
        // Throw object
        // Object is ProjectileAttack (except enable won't be called) => override TriggerEnter

        //    weapon.SetActive(false);
        //    projectileAttack.gameObject.SetActive(true);
        //    projectileAttack.gameObject.transform.parent = null;
        //    projectileAttack.EnableAttack();

        GameObject projectile = GetPooledProjectile();

        if(projectile != null)
        {
            projectile.transform.position = projectileInHand.transform.position;
            projectile.transform.rotation = projectileReference.transform.rotation;
            projectileInHand.SetActive(false);
            projectile.SetActive(true);
            projectile.GetComponent<ProjectileAttack>()?.EnableAttack(origin);
        }
    }

    public override void DisableAttack()
    {
        projectileInHand.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    public GameObject GetPooledProjectile()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledProjectiles[i].activeInHierarchy)
            {
                return pooledProjectiles[i];
            }
        }

        return null;
    }
}
