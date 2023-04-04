using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public int currentWaypoint;
    public double distanceFromWaypoint;
    public int currentLap;
    public int wayPointsCrossed;

    public Vector3 dir;
    public Vector3 pointOnTarget;

    public WayPointManager wayPointManager;
    public SceneManager sceneManager;

    private bool isColliding = false;

    void Start()
    {
        currentWaypoint = 0;
        distanceFromWaypoint = 0;
        wayPointsCrossed = 0;
        dir = Vector3.zero;
        currentLap = 1;
        pointOnTarget = wayPointManager.RandomPointInWaypoint(wayPointManager.mp1Waypoints[0]);
    }

    public Vector3 EvaluateWaypoint()
    {
        dir = pointOnTarget - this.transform.position;
        distanceFromWaypoint = Vector3.Distance(this.transform.position, pointOnTarget);
        if (distanceFromWaypoint < 0.1f)
        {
            wayPointsCrossed++;
            wayPointManager.UpdateWaypoint(this);
        }
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
        if (collider.CompareTag("FinalPoint"))
        {
            // Check if player (likely) finished the second lap
            if (this.gameObject.CompareTag("Player") && wayPointsCrossed > 10)
            {
                // Get time
                this.gameObject.GetComponent<PlayerController>().final = sceneManager.stopwatch;

                // Load game over scene
                sceneManager.GameOver();
            }
            // If not a player, ensure the AI completed the entire course
            else if (wayPointsCrossed > wayPointManager.GetLength(this)/2)
            {
                // Get time
                this.gameObject.GetComponent<EnemyController>().final = sceneManager.stopwatch;
            }
        }
        else if (collider.CompareTag("EndPoint"))
        {
            // Check if player (likely) finished a lap
            if (this.gameObject.CompareTag("Player") && wayPointsCrossed > 10)
            {
                // Reset waypoints crossed
                wayPointsCrossed = 0;
                this.gameObject.GetComponent<PlayerController>().lap1 = sceneManager.stopwatch;

                // Update lap
                currentLap++;
                sceneManager.lapTxt.text = "Lap " + currentLap + " / 2";

                // Send player to new location
                Vector3 newLapPos = new(0, 1000, 0);
                this.transform.position += newLapPos;

                // Update skybox for player
                sceneManager.GetComponent<ChangeSkybox>().enabled = true;

                // Assign new waypoints and update
                currentWaypoint = -1;
                wayPointManager.UpdateWaypoint(this);
            }
            // If not a player, ensure the AI completed the entire course
            else if (wayPointsCrossed > wayPointManager.GetLength(this)/2)
            {
                // Reset waypoints crossed
                wayPointsCrossed = 0;
                this.gameObject.GetComponent<EnemyController>().lap1 = sceneManager.stopwatch;

                // Update lap
                currentLap++;

                // Send enemy to new location
                Vector3 newLapPos = new(0, 1000, 0);
                this.transform.position += newLapPos;

                // Assign new waypoints and update
                currentWaypoint = -1;
                wayPointManager.UpdateWaypoint(this);
            }
        }
        if (this.gameObject.CompareTag("Player") && collider.CompareTag("Waypoint"))
        {
            wayPointsCrossed++;
            wayPointManager.UpdateWaypoint(this);
        }
    }
}