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
    public GameObject _entity;
    private bool MustBeEntity(GameObject gameObject)
    {
        if (gameObject.GetComponentInChildren<EntityBase>() != null)
            return true;
        else
            return false;
    }

    private GameObject _entityGameObject;

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
        _entityGameObject = Instantiate(_entity, transform);

        if(DoPatrol)
        {
            if (patrolWaypoints is null)
            {
                Debug.LogError("Couldn't find patrol waypoints");
                return;
            }
            
            EnemyPatrolling enemyPatrolling = _entityGameObject.GetComponentInChildren<EnemyPatrolling>();

            if (enemyPatrolling is not null)
            {
                enemyPatrolling.SetPatrolWaypoints(patrolWaypoints);
            }
            else
                Debug.LogError("Couldn't find enemy patrolling script");
        }

        _entityGameObject.SetActive(false);
    }

    public void ActivateEntity()
    {
        _entityGameObject.SetActive(true);
        _entityGameObject.transform.position = transform.position;
    }

    public void DeactivateEntity()
    {
        _entityGameObject.SetActive(false);
    }
}
