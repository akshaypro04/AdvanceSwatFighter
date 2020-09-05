using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour
{
    WayPoint[] wayPoints;
    public event System.Action<WayPoint> OnWayPointChange;

    int currentWayPointIndex = -1;

    void Awake()
    {
        wayPoints = GetWayPoints();
    }

    public void SetNextWayPoint()
    {
        currentWayPointIndex += 1;

        if (currentWayPointIndex == wayPoints.Length)
            currentWayPointIndex = 0;

        if (OnWayPointChange != null)
            OnWayPointChange(wayPoints[currentWayPointIndex]);
    }


    WayPoint[] GetWayPoints()
    {
        return GetComponentsInChildren<WayPoint>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 perviousWayPoint = Vector3.zero;
        foreach(var waypoint in GetWayPoints())
        {
            Vector3 waypointPosition = waypoint.transform.position;
            Gizmos.DrawWireSphere(waypointPosition, 0.5f);
            if (perviousWayPoint != Vector3.zero)
                Gizmos.DrawLine(perviousWayPoint, waypointPosition);

            perviousWayPoint = waypointPosition;
        }

    }
}
