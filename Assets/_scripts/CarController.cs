
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{ 
    private CarBasicMovement controls;
    [SerializeField] private DriveType_So driveType;
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] float acceleration = 500f;
    [SerializeField] float maxBrakeTorque = 3000f;
    [SerializeField] float maxTurnAngle = 15f;
    [SerializeField] float maxSpeed =10f;
    [SerializeField] float accelerationRate = 1f;
    [SerializeField] float brakingRate = 1f;
    [SerializeField] float maxAcceleration = 500f; // Maximum acceleration value
    [SerializeField] Transform frontRightWheelMesh;
    [SerializeField] Transform frontLeftWheelMesh;
    [SerializeField] Transform backRightWheelMesh;
    [SerializeField] Transform backLeftWheelMesh;
    [SerializeField] AudioSource idelSound;
    [SerializeField] AudioSource runningSound;
    [SerializeField] WheelCollider wheelCollider;

    public float idelMaxVolume;
    public float minPitch = 0.5f;
    public float maxPitch = 2f;
    float handbrakeInput = 0;
    float currentTurnAngle = 0;

    private float accelerationInput = 0f;
    
    Rigidbody rb;

   

    void Awake()
    {
        controls = new CarBasicMovement();
        controls.Player.HandBreak.performed += ctx => HandbrakeInput(ctx.ReadValue<float>());
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        //Debug.Log(controls.Player.Move.ReadValue<Vector2>());
        //Debug.Log(controls.Player.HandBreak.ReadValue<float>());
      
    float accelerationInput = controls.Player.Move.ReadValue<Vector2>().y; 
    //Debug.Log("Acceleration Input: " + accelerationInput);

    float accelerationTorque = accelerationInput * driveType.maxAccelerationTorque;
    

    // Get player input for steering
    float steeringInput = controls.Player.Move.ReadValue<Vector2>().x; 
    float targetTurnAngle = maxTurnAngle * steeringInput;
    currentTurnAngle = Mathf.Lerp(currentTurnAngle, targetTurnAngle, Time.deltaTime * 1);

    // Get player input for handbrake
    handbrakeInput = controls.Player.HandBreak.ReadValue<float>();
    float brakeTorque = driveType.maxBrakeTorque;

    if (handbrakeInput > 0)
    {
        float handbrakeTorque = handbrakeInput * brakeTorque;
        frontRight.brakeTorque = handbrakeTorque;
        frontLeft.brakeTorque = handbrakeTorque;
        backRight.brakeTorque = handbrakeTorque;
        backLeft.brakeTorque = handbrakeTorque;
    }
    else
    {
        frontRight.brakeTorque = 0;
        frontLeft.brakeTorque = 0;
        backRight.brakeTorque = 0;
        backLeft.brakeTorque = 0;
    }
    float currentSpeed = rb.velocity.magnitude;
    //Debug.Log("speed"+currentSpeed.ToString("F2"));

    float speedLimit = 15.0f;
    if(currentSpeed > speedLimit && accelerationInput > 0)
    {
        accelerationTorque = 0;
    }
   
        switch (driveType.driveType)
        {
            case DriveType_So.DriveType.FrontWheel:
                frontRight.motorTorque = accelerationTorque;
                frontLeft.motorTorque = accelerationTorque;
                break;
            case DriveType_So.DriveType.RearWheel:
                backRight.motorTorque = accelerationTorque;
                backLeft.motorTorque = accelerationTorque;
                break;
            case DriveType_So.DriveType.AllWheel:
                frontRight.motorTorque = accelerationTorque;
                frontLeft.motorTorque = accelerationTorque;
                backRight.motorTorque = accelerationTorque;
                backLeft.motorTorque = accelerationTorque;
                break;
        }
        if(accelerationInput ==0)
        {
            rb.velocity *= 1f;
        }
         //Debug.Log("Car Velocity: " + rb.velocity.magnitude);
    
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        // Update wheel positions
        SetWheel(frontRight, frontRightWheelMesh);
        SetWheel(frontLeft, frontLeftWheelMesh);
        SetWheel(backRight, backRightWheelMesh);
        SetWheel(backLeft, backLeftWheelMesh); 
    }
    void Update()
    {
        float speedKPH = GetSpeed(wheelCollider);
        // Check if the car is stationary
        if (speedKPH == 0)
        {
            // Play idle sound if it's not already playing
            if (!idelSound.isPlaying)
            {
                idelSound.Play();
            }
            // Stop running sound if it's playing
            if (runningSound.isPlaying)
            {
                runningSound.Stop();
            }
        }
        else
        {
            // Stop idle sound if it's playing
            if (idelSound.isPlaying)
            {
                idelSound.Stop();
            }
            // Play running sound if it's not already playing
            if (!runningSound.isPlaying)
            {
                runningSound.Play();
            }
        }
        float pitch = Mathf.Lerp(minPitch, maxPitch, speedKPH / 100f); // Adjust divisor to set the speed sensitivity
        runningSound.pitch = pitch;
    }

    
    void SetWheel(WheelCollider wheelCol, Transform wheelMesh)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCol.GetWorldPose(out pos, out rot);

        wheelMesh.position = pos;
        wheelMesh.rotation = rot;
    }
     public void HandbrakeInput(float value)
    {
        handbrakeInput = Mathf.Clamp(value, 0f, 1f);;
         Debug.Log("Handbrake input: " + handbrakeInput);
    }
    
    
      public float GetSpeed(WheelCollider wheelCollider)
    {
        // Get the radius of the wheel
        float radius = wheelCollider.radius;

        // Calculate the speed in meters per second
        float angularVelocity = wheelCollider.rpm * 2 * Mathf.PI / 60;

        float linearVelocity = angularVelocity * radius;

        // Convert speed to kilometers per hour (optional)
        float speedKPH = linearVelocity * 3.6f;
        return speedKPH;
    }
    public void Stop()
    {
     
        accelerationInput = 0f;
    
        
        float handbrakeTorque = 1f * maxBrakeTorque;
        frontRight.brakeTorque = handbrakeTorque;
        frontLeft.brakeTorque = handbrakeTorque;
        backRight.brakeTorque = handbrakeTorque;
        backLeft.brakeTorque = handbrakeTorque;
    }
    
    

}
