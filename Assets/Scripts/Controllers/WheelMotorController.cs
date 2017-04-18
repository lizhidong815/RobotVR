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
	public bool setmotor = false;
    private void Awake()
    {
        wheels = new List<Wheel>();
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
		foreach (Wheel wheel in wheels) {
			wheel.FixedUpdate ();
		}
		if (setmotor) {
			SetMotorSpeed (0, wheels [0].speed);
			SetMotorSpeed (1, wheels [1].speed);
			setmotor = false;
		}
	}


}