using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        SortWaypoints();
        currentWaypointTarget = waypoints[0];
    }

    public Vector3 EvaluateWaypoint()
    {
        float dirX = 0;
        dir = waypoints[currentWaypoint].transform.position - this.transform.position;
        dir.x += dirX;
        dir.y += dirX;
        dir.z += dirX;

        dir.Normalize();
        return dir;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (waypoints.Contains(collider.gameObject))
        {
            int index = System.Array.FindIndex(waypoints, x => x.name == collider.gameObject.name);
            if (currentWaypoint == index)
            {
                currentWaypoint++;
                currentWaypoint %= waypoints.Length;
            }
        }
    }

    private void SortWaypoints()
    {
        waypoints = waypoints.OrderBy(x => x.name).ToArray();
    }

    // Debug
    private void PrintWaypoints()
    {
        foreach (GameObject waypoint in waypoints)
        {
            Debug.Log(waypoint.name);
        }
    }
}