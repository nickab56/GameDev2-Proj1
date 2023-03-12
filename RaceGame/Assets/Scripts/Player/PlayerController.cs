using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public CheckPointManager CPManager;
    public GameObject Manager;

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
        Manager = GameObject.FindGameObjectWithTag("SceneManager");
        CPManager = Manager.GetComponentInChildren<CheckPointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        steer();
        groundNormalRotation();
        //drift();
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

    private void groundNormalRotation()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.75f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 7.5f * Time.deltaTime);
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }
    }

    private void drift()
    {
        if(Input.GetKeyDown(KeyCode.V) && touchingGround)
        {
            if(steerDirection > 0 )
            {
                driftRight = true;
                driftLeft = false;
            }
            else if (steerDirection < 0)
            {
                driftRight = false;
                driftLeft = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.V) && touchingGround && currentSpeed > 40 && Input.GetAxis("Horizontal") != 0)
        {
            driftTime += Time.deltaTime;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.CompareTag("CheckPoint 1"))
        {
            CPManager.boolPoint1 = true;
        }

        if(other.transform.gameObject.CompareTag("CheckPoint 2") && CPManager.boolPoint1 == true)
        {
            CPManager.boolPoint2 = true;
        }

        if (other.transform.gameObject.CompareTag("CheckPoint 3") && CPManager.boolPoint2 == true)
        {
            CPManager.boolPoint3 = true;
        }

        if (other.transform.gameObject.CompareTag("CheckPoint 4") && CPManager.boolPoint3 == true)
        {
            CPManager.boolPoint4 = true;
        }

        if (other.transform.gameObject.CompareTag("CheckPoint 5") && CPManager.boolPoint4 == true)
        {
            CPManager.boolPoint5 = true;
        }

        if (other.transform.gameObject.CompareTag("CheckPoint 6") && CPManager.boolPoint5 == true)
        {
            CPManager.boolPoint6 = true;
        }

        if (other.transform.gameObject.CompareTag("CheckPoint 7") && CPManager.boolPoint6 == true)
        {
            CPManager.boolPoint7 = true;
        }

        if (other.transform.gameObject.CompareTag("CheckPoint 8") && CPManager.boolPoint7 == true)
        {
            CPManager.boolPoint8 = true;
        }

        if (other.transform.gameObject.CompareTag("FinalPoint") && CPManager.boolPoint8 == true)
        {
            CPManager.boolFinal = true;
        }
    }
}
