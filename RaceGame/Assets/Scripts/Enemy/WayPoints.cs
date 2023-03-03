using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public GameObject[] waypoints;
    public Vector3 dir;

    int currentWaypoint = 0;
    GameObject currentWaypointTarget;

    private EnemyController EnemyAi;

    void Start()
    {
        EnemyAi = this.GetComponent<EnemyController>();
        dir = Vector3.zero;
        currentWaypointTarget = waypoints[0];
    }

    public Vector3 EvaluateWaypoint()
    {
        float dirX = 0;
        dir = waypoints[currentWaypoint].transform.position - this.transform.position;
        dir.x += dirX;
        dir.y += dirX;
        dir.z += dirX;

        if (dir.magnitude < 0.01) //enemy has reached the waypoint
        {
            currentWaypoint++;
            currentWaypoint %= waypoints.Length;
        }
        dir.Normalize();
        return dir;
    }

    private void FindClosest()
    {
        float shortestDistance = float.MaxValue;
        Vector3 shortestDirection = Vector3.zero;
        Vector3 DirTemp = Vector3.zero;
        for (int i = 0; i < waypoints.Length; i++)
        {
            DirTemp = waypoints[i].transform.position - this.transform.position;
            DirTemp.z = 0;
            float distance = DirTemp.magnitude;
            if (distance < shortestDistance)
            {
                DirTemp.z = 0;
                shortestDirection = DirTemp;
                shortestDistance = distance;
                currentWaypointTarget = waypoints[i];
                currentWaypoint = (i > 0) ? i : waypoints.Length - 1;
            }
        }
        dir = shortestDirection;
    }
    public Vector3 FindClosestWaypoint()
    {
        Vector3 foo = this.transform.position - currentWaypointTarget.transform.position;
        float DistanceToTarget = foo.magnitude;

        FindClosest();

        return dir.normalized;
    }
    // Update is called once per frame
    void Update()
    {

    }
}