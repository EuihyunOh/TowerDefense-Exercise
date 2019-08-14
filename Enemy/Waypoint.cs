using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Waypoint nextWaypoint;

    void start()
    {
        if(nextWaypoint == null)
        {
            //nextWaypoint = this;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Waypoint GetNextWaypoint()
    {
        return nextWaypoint;
    }
}
