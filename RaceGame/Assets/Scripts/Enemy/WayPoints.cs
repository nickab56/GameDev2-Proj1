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
    Vector3 pointOnTarget;

    private EnemyController EnemyAi;
    private bool isColliding = false;

    void Start()
    {
        EnemyAi = this.GetComponent<EnemyController>();
        dir = Vector3.zero;
        SortWaypoints();
        currentWaypointTarget = waypoints[0];
        pointOnTarget = RandomPointInWaypoint(waypoints[currentWaypoint]);
    }

    public Vector3 EvaluateWaypoint()
    {
        dir = pointOnTarget - this.transform.position;
        //dir = waypoints[currentWaypoint].transform.position - this.transform.position;
        dir.Normalize();
        return dir;
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isColliding) return;
        isColliding = true;
        if (waypoints.Contains(collider.gameObject))
        {
            currentWaypoint++;
            currentWaypoint %= waypoints.Length;
            pointOnTarget = RandomPointInWaypoint(waypoints[currentWaypoint]);
        }
        StartCoroutine(Reset());
    }

    private void SortWaypoints()
    {
        waypoints = waypoints.OrderBy(x => x.name).ToArray();
    }

    private Vector3 RandomPointInWaypoint(GameObject waypoint)
    {
        Bounds bounds = waypoint.GetComponent<BoxCollider>().bounds;
        Vector3 point = new(Random.Range(bounds.min.x, bounds.max.x), this.transform.position.y, Random.Range(bounds.min.z, bounds.max.z));
        return point;
    }
    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }

}