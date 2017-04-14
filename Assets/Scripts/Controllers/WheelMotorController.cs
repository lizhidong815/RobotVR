using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RobotCommands;
using RobotComponents;

public class WheelMotorController : MonoBehaviour, 
    IMotorControl, 
    IPIDControl
{
    public List<WheelInfo> wheels; // the information about each individual wheel  
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    private void Awake()
    {
        wheels = new List<WheelInfo>();
    }

    // Set the local PID Parameters
    public void SetPIDParams(int motor, int p, int i, int d)
    {
        wheels[motor].pidEnabled = true;
        wheels[motor].P = p;
        wheels[motor].I = i;
        wheels[motor].D = d;
    }
    // Set the speed of a single motor
    public void SetMotorSpeed(int motor, int speed)
    {
        wheels[motor].speed = speed;
        wheels[motor].wheel.motorTorque = maxMotorTorque * speed;
    }

    // Set the speed of a single motor (controlled)
    public void SetMotorControlled(int motor, int ticks)
    {
        if(!wheels[motor].pidEnabled)
        {
            SetPIDParams(motor, 4, 1, 1);
        }

        SetMotorSpeed(motor, ticks);
    }

    // Function to update visuals of wheels
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    // Update visual of wheel on each frame
    public void FixedUpdate()
    {
        foreach(WheelInfo wheel in wheels)
        {
            ApplyLocalPositionToVisuals(wheel.wheel);
        }
    }
}