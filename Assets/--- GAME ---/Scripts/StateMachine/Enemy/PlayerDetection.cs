using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private PlayerBase _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBase player))
        {
            _player = player;
        }
    }

    public bool IsPlayerDetected()
    {
        return _player != null;
    }

    public Vector3 GetTargetPosition()
    {
        if (_player == null)
            Debug.LogError("Player is null, check before using Get Target Position");

        return _player.transform.position;
    }

    public void EmptyTarget()
    {
        _player = null;
    }
}
