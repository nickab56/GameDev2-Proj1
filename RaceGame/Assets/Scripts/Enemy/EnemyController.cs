using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum AIType { waypoints, none };
    public enum AIDifficulty { NONE, EASY, MEDIUM, HARD }
    
    public AIDifficulty aIDifficulty = AIDifficulty.NONE;
    public AIType aiType = AIType.none;
    public Vector3 dir;

    public int position;
    public float lap1;
    public float lap2;
    public float final;

    public float currentSpeed = 0;

    private float maxSpeed;
    private float lerpConstant;
    private WayPoints waypoints;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = this.GetComponent<WayPoints>();
        player = GameObject.FindGameObjectWithTag("Player");
        // Set max speed and lerp constant based on enemy's difficulty
        maxSpeed = GetMaxSpeed();
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
            this.transform.forward = Vector3.Lerp(this.transform.forward, dir, 5.0f * Time.deltaTime);
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
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, lerpConstant * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed/1.5f, lerpConstant * Time.deltaTime);
        }
    }

    float GetMaxSpeed()
    {
        switch (aIDifficulty)
        {
            case AIDifficulty.EASY:
                lerpConstant = 0.5f;
                return Random.Range(15f, 17f); // 15, 17
            case AIDifficulty.MEDIUM:
                lerpConstant = 0.25f;
                return Random.Range(22.5f, 26f); // 22.5, 26
            case AIDifficulty.HARD:
                lerpConstant = 0.1f;
                return Random.Range(30f, 35f); // 30, 35
            default:
                break;
        }
        return 0f;
    }
}