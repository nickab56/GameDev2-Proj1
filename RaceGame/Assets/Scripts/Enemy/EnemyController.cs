using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum AIType { waypoints, none };
    public AIType aiType = AIType.none;
    public Vector3 dir;

    public float maxSpeeed = 12;
    public float currentSpeed = 0;

    private WayPoints waypoints;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = this.GetComponent<WayPoints>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Vector3.zero;
        if (player != null)
        {
            dir = ProcessAI();
            temp = currentSpeed * Time.deltaTime * dir;
        }

        this.transform.position += new Vector3(temp.x, 0, temp.z);
        
        if (dir != Vector3.zero)
        {
            this.transform.forward = Vector3.Lerp(this.transform.forward, dir, 7.0f * Time.deltaTime);
        }
    }

    Vector3 ProcessAI()
    {
        Vector3 returnDir = Vector3.zero;
        CheckPlayerView();
        switch (aiType)
        {
            case AIType.waypoints:
                returnDir = waypoints.EvaluateWaypoint();
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

    private void CheckPlayerView()
    {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);

        if (dist <= 50.0f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeeed, 0.5f * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeeed/3, 0.5f * Time.deltaTime);
        }
    }
}