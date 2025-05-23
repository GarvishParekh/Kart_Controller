using UnityEngine;

public class WheelColliderController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody kartRb;

    [Header("Scriptable")]
    [SerializeField] private InputData inputData;
    [SerializeField] private KartData kartData;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;
    [SerializeField] private WheelCollider rearRightCollider;

    [Header("Wheel Meshes")]
    [SerializeField] private Transform frontLeftMesh;
    [SerializeField] private Transform frontRightMesh;
    [SerializeField] private Transform rearLeftMesh;
    [SerializeField] private Transform rearRightMesh;

    [Header("Car Settings")]
    [SerializeField] private float maxMotorTorque = 1500f; // Adjust for high acceleration
    [SerializeField] private float maxSteerAngle = 30f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private float downForce = 100;

    float motorInput;
    float steerInput;
    float brakeInput;

    private void Awake()
    {
        kartRb.centerOfMass = new Vector3(0, -0.3f, 0);
    }

    void Update()
    {
        switch (inputData.controlType)
        {
            case ControlType.KEYBOARD:
                motorInput = inputData.zKeybaord;
                steerInput = inputData.xKeybaord;  
                brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f; // Space for brake
                break;
            case ControlType.GYRO:
                motorInput = inputData.zButton;
                steerInput = inputData.xGyro;
                break;
        }
        // Get player input
    }

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        AppleDownForce();
    }

    private void HandleMotor()
    {
        // Apply motor torque to rear wheels (AWD)
        rearLeftCollider.motorTorque = motorInput * maxMotorTorque;
        rearRightCollider.motorTorque = motorInput * maxMotorTorque;
        frontLeftCollider.motorTorque = motorInput * maxMotorTorque;
        frontRightCollider.motorTorque = motorInput * maxMotorTorque;

        // Apply brakes to all wheels
        float appliedBrake = brakeInput * brakeForce;
        frontLeftCollider.brakeTorque = appliedBrake;
        frontRightCollider.brakeTorque = appliedBrake;
        rearLeftCollider.brakeTorque = appliedBrake;
        rearRightCollider.brakeTorque = appliedBrake;
    }

    private void HandleSteering()
    {
        float velocityMagnitude = kartRb.velocity.magnitude;
        maxSteerAngle = Remap(velocityMagnitude, 0, 30, kartData.kartMaxTurn, kartData.kartMinTurn);
        // Apply steer angle to front wheels only
        float steer = steerInput * maxSteerAngle;
        frontLeftCollider.steerAngle = steer;
        frontRightCollider.steerAngle = steer;
    }

    private void UpdateWheels()
    {
        UpdateWheelPose(frontLeftCollider, frontLeftMesh);
        UpdateWheelPose(frontRightCollider, frontRightMesh);
        UpdateWheelPose(rearLeftCollider, rearLeftMesh);
        UpdateWheelPose(rearRightCollider, rearRightMesh);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        mesh.position = pos;
        mesh.rotation = rot;
    }
    
    private void AppleDownForce()
    {
        float speed = kartRb.velocity.magnitude;
        kartRb.AddForce(-transform.up * downForce * speed);
    }

    public float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        float t = Mathf.InverseLerp(fromMin, fromMax, value); // Normalize to 0–1
        return Mathf.Lerp(toMin, toMax, t); // Remap to new range
    }
}
