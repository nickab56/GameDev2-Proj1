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
            currentWaypoint++;
            currentWaypoint %= waypoints.Length;
        }
    }
}