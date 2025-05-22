using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelColliderController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    [Header("Wheel Meshes")]
    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    [Header("Car Settings")]
    public float maxMotorTorque = 1500f; // Adjust for high acceleration
    public float maxSteerAngle = 30f;
    public float brakeForce = 3000f;

    private float motorInput;
    private float steerInput;
    private float brakeInput;

    void Update()
    {
        // Get player input
        motorInput = Input.GetAxis("Vertical");   // W/S or Up/Down Arrows
        steerInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrows
        brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f; // Space for brake
    }

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleMotor()
    {
        // Apply motor torque to rear wheels (RWD)
        rearLeftCollider.motorTorque = motorInput * maxMotorTorque;
        rearRightCollider.motorTorque = motorInput * maxMotorTorque;

        // Apply brakes to all wheels
        float appliedBrake = brakeInput * brakeForce;
        frontLeftCollider.brakeTorque = appliedBrake;
        frontRightCollider.brakeTorque = appliedBrake;
        rearLeftCollider.brakeTorque = appliedBrake;
        rearRightCollider.brakeTorque = appliedBrake;
    }

    private void HandleSteering()
    {
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
}
