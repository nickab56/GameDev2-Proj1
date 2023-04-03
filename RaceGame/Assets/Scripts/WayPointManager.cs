using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    public GameObject[] mp1Waypoints;
    public GameObject[] mp2Waypoints;

    // Start is called before the first frame update
    void Start()
    {
        SortWaypoints(mp1Waypoints);
        SortWaypoints(mp2Waypoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SortWaypoints(GameObject[] waypoints)
    {
        waypoints = waypoints.OrderBy(x => x.name).ToArray();
    }

    public Vector3 RandomPointInWaypoint(GameObject waypoint)
    {
        Bounds bounds = waypoint.GetComponent<BoxCollider>().bounds;
        Vector3 point = new(Random.Range(bounds.min.x, bounds.max.x), waypoint.transform.position.y, Random.Range(bounds.min.z, bounds.max.z));
        return point;
    }

    public int GetLength(WayPoints racer)
    {
        if (racer.currentLap == 1)
        {
            return mp1Waypoints.Length;
        }
        return mp2Waypoints.Length;
    }

    public void UpdateWaypoint(WayPoints racer)
    {
        racer.currentWaypoint++;
        if (racer.currentLap == 1)
        {
            racer.currentWaypoint %= mp1Waypoints.Length;
            racer.pointOnTarget = RandomPointInWaypoint(mp1Waypoints[racer.currentWaypoint]);
        } else
        {
            racer.currentWaypoint %= mp2Waypoints.Length;
            racer.pointOnTarget = RandomPointInWaypoint(mp2Waypoints[racer.currentWaypoint]);
        }
    }
}
