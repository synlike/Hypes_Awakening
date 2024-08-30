using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EntitySpawner : MonoBehaviour, ISpawnable
{
    [Title("Prefab")]
    [ValidateInput("MustBeEntity", "This field should be an entity prefab.")]
    public GameObject prefab;
    private bool MustBeEntity(GameObject gameObject)
    {
        if (gameObject.GetComponentInChildren<EntityBase>() != null)
            return true;
        else
            return false;
    }

    private GameObject entityGameObject;
    private EntityBase entity;

    [Title("Death Infos")]
    public bool DoPatrol = false;
    [ShowIf("DoPatrol")]
    public List<Transform> patrolWaypoints;


    void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        entityGameObject = Instantiate(prefab, transform);
        entity = entityGameObject.GetComponentInChildren<EntityBase>();

        if (entity is null)
            Debug.LogError("Couldn't find entity base in prefab");

        if(DoPatrol)
        {
            if (patrolWaypoints is null)
            {
                Debug.LogError("Couldn't find patrol waypoints");
                return;
            }
            
            EnemyPatrolling enemyPatrolling = entityGameObject.GetComponentInChildren<EnemyPatrolling>();

            if (enemyPatrolling is not null)
            {
                enemyPatrolling.SetPatrolWaypoints(patrolWaypoints);
            }
            else
                Debug.LogError("Couldn't find enemy patrolling script");
        }

        entityGameObject.SetActive(false);
    }

    public void ActivateEntity()
    {
        entity.transform.position = transform.position;
        entityGameObject.SetActive(true);
    }
}
