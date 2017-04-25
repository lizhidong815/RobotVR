using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RobotCommands;
using RobotComponents;

public class WheelMotorController : MonoBehaviour, 
    IMotorControl, 
    IPIDControl
{
    public List<Wheel> wheels; // the information about each individual wheel  
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
	public float wheelDist;
	public float estimatedrot;
	public Vector3 estimatedpos;

    private void Awake()
    {
        wheels = new List<Wheel>();
		estimatedpos = new Vector3();
    }

    // Set the local PID Parameters
    public void SetPIDParams(int motor, int p, int i, int d)
    {
		wheels [motor].SetPIDParams (p, i, d);
    }
    // Set the speed of a single motor
    public void SetMotorSpeed(int motor, float speed)
    {
		wheels [motor].SetMotorSpeed (speed);
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
    // Update visual of wheel on each frame
    public void FixedUpdate()
	{
		float leftdist = wheels [0].tickrate / 540 * Mathf.PI * wheels [0].diameter;
		float rightdist = wheels [1].tickrate / 540 * Mathf.PI * wheels [1].diameter;
		float turnAngle = (leftdist - rightdist) * 360 / Mathf.PI / 2 / wheelDist;
		estimatedrot += turnAngle;
		//float turnRadius = (leftdist + rightdist) / 2 * 360 / turnAngle / 2 / Mathf.PI;
		//float straightdist = 2 * turnRadius * Mathf.Sin (Mathf.Deg2Rad * turnAngle / 2);
		float straightdist = (leftdist + rightdist)/2;
		if (!float.IsNaN (straightdist)) {
			estimatedpos += new Vector3 (straightdist * Mathf.Sin (Mathf.Deg2Rad * turnAngle / 2), 0, straightdist * Mathf.Cos (Mathf.Deg2Rad * turnAngle / 2));
		}
	}


}