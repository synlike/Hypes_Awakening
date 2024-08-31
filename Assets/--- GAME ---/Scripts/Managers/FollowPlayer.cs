using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    void LateUpdate()
    {
        transform.position = playerTransform.position;
    }
}
