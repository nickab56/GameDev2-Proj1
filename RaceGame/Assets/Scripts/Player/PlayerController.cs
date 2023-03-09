using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public CheckPointManager CPManager;
    public SceneManager Manager;

    [Header("Player Speeds")]
    public float maxSpeeed;
    private float currentSpeed = 0;
    private float realSpeed; //This speed will deal with amount of steering || I didn't involve it yet :) ||
    public float burstSpeed;

    [Header("Tires")]
    public Transform frontLeftTire;
    public Transform frontRightTire;
    public Transform backLeftTire;
    public Transform backRightTire;

    private float steerDirection;
    private float driftTime;

    private bool driftLeft = false;
    private bool driftRight = false;
    private bool Sliding = false;

    private bool touchingGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        steer();
    }

    private void move()
    {
        realSpeed = transform.InverseTransformDirection(rb.velocity).z;

        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeeed, 0.5f * Time.deltaTime); //active speed
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeeed / 2, 0.5f * Time.deltaTime); //Backing up
        }
        else 
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.5f * Time.deltaTime); // Slow down to stop
        }

        Vector3 velocity = transform.forward * currentSpeed;
        velocity.y = rb.velocity.y; //Allows the player to have gravity
        rb.velocity = velocity;
    }

    private void steer()
    {
        steerDirection = Input.GetAxis("Horizontal"); // gives a value of -1, 0, 1
        Vector3 VecDirection; 
        

        VecDirection = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerDirection *20, transform.eulerAngles.z);

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, VecDirection, 3 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.CompareTag("CheckPoint 1"))
        {
            Manager.GetComponentInChildren<CheckPointManager>().boolPoint1 = false;
        }
    }
}
