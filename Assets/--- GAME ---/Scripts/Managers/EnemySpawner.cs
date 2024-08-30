using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemySpawner : MonoBehaviour, ISpawnable
{
    [Title("Prefab")]
    [ValidateInput("MustBeEntity", "This field should be an entity prefab.")]
    public GameObject _enemy;
    private bool MustBeEntity(GameObject gameObject)
    {
        if (gameObject.GetComponentInChildren<EntityBase>() != null)
            return true;
        else
            return false;
    }

    private GameObject _enemyGameObject;

    void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        _enemyGameObject = Instantiate(_enemy, transform);
        _enemyGameObject.SetActive(false);
    }

    public void ActivateEntity()
    {
        _enemyGameObject.SetActive(true);
        _enemyGameObject.transform.position = transform.position;
    }

    public void DeactivateEntity()
    {
        _enemyGameObject.SetActive(false);
    }
}
