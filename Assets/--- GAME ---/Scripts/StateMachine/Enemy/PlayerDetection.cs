using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public PlayerBase Player { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBase player))
        {
            Player = player;
        }
    }
}
