using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum AIType { waypoints, attack, none };
    public AIType aiType = AIType.none;
    public float speed = 3;

    private WayPoints waypoints;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = this.GetComponent<WayPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Vector3.zero;
        // Normal movement 
        if (player != null)
        {
            temp = speed * Time.deltaTime * ProcessAI();
        }

        transform.position += new Vector3(temp.x, temp.y, 0);
    }

    Vector3 ProcessAI()
    {
        Vector3 dir = player.transform.position - this.transform.position;

        // Function that checks if player is within enemy sites
        Vector3 returnDir = CheckPlayerView();
        switch (aiType)
        {
            case AIType.none:
                break;
            case AIType.waypoints:
                returnDir = waypoints.EvaluateWaypoint();
                break;
            case AIType.attack:
                returnDir = VectorTrack(dir);
                break;
            default:
                break;
        }
        if (Mathf.Abs(returnDir.x) < 0.1)
        {
            returnDir.x = 0;
        }
        if (Mathf.Abs(returnDir.y) < 0.1)
        {
            returnDir.y = 0;
        }
        if (Mathf.Abs(returnDir.z) < 0.1)
        {
            returnDir.z = 0;
        }
        return returnDir;
    }

    private Vector3 VectorTrack(Vector3 rawDirection)
    {
        Vector3 temp = new(rawDirection.x, rawDirection.y, rawDirection.z);
        temp.Normalize();
        return temp;
    }

    private Vector3 CheckPlayerView()
    {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);

        if (dist <= 5.0f)
        {
            aiType = AIType.attack;
        }

        if (aiType == AIType.attack && dist > 5.0f)
        {
            aiType = AIType.waypoints;
            return waypoints.FindClosestWaypoint();
        }
        return Vector3.zero;
    }
}