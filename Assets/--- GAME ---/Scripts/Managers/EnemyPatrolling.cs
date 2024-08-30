using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    public List<Transform> Waypoints { get; private set; } = new List<Transform>();

    public void SetPatrolWaypoints(List<Transform> waypoints)
    {
        this.Waypoints = waypoints;
    }
}
