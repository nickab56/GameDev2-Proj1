using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public int currentWaypoint;
    public double distanceFromWaypoint;
    public int currentLap;

    public Vector3 dir;
    public Vector3 pointOnTarget;

    public WayPointManager wayPointManager;

    private bool isColliding = false;

    void Start()
    {
        currentWaypoint = 0;
        distanceFromWaypoint = 0;
        dir = Vector3.zero;
        currentLap = 1;
        isColliding = false;
        pointOnTarget = wayPointManager.RandomPointInWaypoint(wayPointManager.mp1Waypoints[0]);
    }

    public Vector3 EvaluateWaypoint()
    {
        dir = pointOnTarget - this.transform.position;
        distanceFromWaypoint = Vector3.Distance(this.transform.position, pointOnTarget);
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
        if (collider.CompareTag("Waypoint"))
        {
            isColliding = true;
            wayPointManager.UpdateWaypoint(this);
        }
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }

}