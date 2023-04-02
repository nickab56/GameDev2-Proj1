using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Game Rules")]

    public CheckPointManager CPManager;
    public GameObject Manager;

    public AudioSource CarEngine;

    public int position;
    // Times are in seconds
    public float lap1 = 0;
    public float final = 7200;

    private float horizontalInput;
    public float verticalInput;
    private float steerAngle;
    private bool isBreaking;
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    [Header("Transform Colliders")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;
    [Header("Player Speeds")]
    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;

    private WayPoints waypoints;

    private void Start()
    {
        waypoints = this.GetComponent<WayPoints>();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        Vector3 tmp = waypoints.EvaluateWaypoint();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetAxis("Vertical") != 0)
        {
            if (CarEngine.isPlaying != true)
                CarEngine.Play();
        }
        else
            CarEngine.Stop();
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
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
