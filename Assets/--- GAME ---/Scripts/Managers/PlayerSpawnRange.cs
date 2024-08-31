using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerSpawnRange : MonoBehaviour
{
    private SphereCollider _collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ISpawnable spawnable))
        {
            spawnable.ActivateEntity();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EntityBase entity))
        {
            //entity.transform.parent.gameObject.SetActive(false);
        }
    }
}
