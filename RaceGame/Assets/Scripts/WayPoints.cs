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
    private int wayPointsCrossed;

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
        dir.Normalize();
        return dir;
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
        Debug.Log("Racer: " + this.gameObject.name + " Current Waypoint: " + currentWaypoint);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isColliding) return;
        isColliding = true;
        if (collider.CompareTag("FinalPoint"))
        {
            if (wayPointsCrossed == wayPointManager.GetLength(this))
            {
                if (this.gameObject.CompareTag("Enemy"))
                {
                    // Destroy Game Object
                    //Destroy(this.gameObject);
                }
                else if (this.gameObject.CompareTag("Player"))
                {
                    // If player, load game over scene
                    Constants.C.RaceFinished = true;
                    StartCoroutine(LoadGameOver());
                }

            }
        }
        else if (collider.CompareTag("EndPoint"))
        {
            if (wayPointsCrossed == wayPointManager.GetLength(this))
            {
                // Waypoints Crossed Reset
                wayPointsCrossed = 0;

                // Get time for player
                

                // Update lap
                currentLap++;

                // Send player to new location
                Vector3 newLapPos = new(0, 1000, 0);
                this.transform.position += newLapPos;

                // Assign new waypoints and update
                currentWaypoint = -1;
                wayPointManager.UpdateWaypoint(this);
            }
        }
        else if (collider.CompareTag("Waypoint"))
        {
            wayPointsCrossed++;
            wayPointManager.UpdateWaypoint(this);
        }
    }
    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }
}