using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotComponents;

// Robot Pose
[System.Serializable]
public class Pose
{
    public int x;
    public int y;
    public int phi;

    public Pose(int x, int y, int phi)
    {
        this.x = x;
        this.y = y;
        this.phi = phi;
    }
}

public class WheelMotorController : MonoBehaviour
{
    public List<Wheel> wheels; // the information about each individual wheel  
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float wheelDist;
    public float Rot;
    public float w;
    public Vector3 Pos;
    public float v;

    public bool drive = false; // for testing purposes

    //vw parameters
    public float targetDist;
    public float travelledDist;
    public float targetRot;
    public float travelledRot;
    public string checkType;
    public bool checkActive;

    public Action DriveDoneDelegate;

    private void Awake()
    {
       // wheels = new List<Wheel>();
        Pos = new Vector3();
    }

    // Set the local PID Parameters
    public void SetPIDParams(int motor, int p, int i, int d)
    {
        wheels[motor].SetPIDParams(p, i, d);
    }
    // Set the speed of a single motor
    public void SetMotorSpeed(int motor, float speed)
    {
        wheels[motor].SetMotorSpeed(speed);
    }

    // Set the speed of a single motor (controlled)
    public void SetMotorControlled(int motor, int ticks)
    {
        if (!wheels[motor].pidEnabled)
        {
            SetPIDParams(motor, 4, 1, 1);
        }

        SetMotorSpeed(motor, ticks);
    }
    // Update visual of wheel on each frame
    private void FixedUpdate()
    {
        if (drive)
        { //for testing purposes
            DriveCurve(1, 90, 0.1f);
            drive = false;
        }

        updatePosition();
        checkDrive();
    }

    public void DriveStraight(float distance, float velocity)
    {
        resetController();
        targetDist = distance;
        checkType = "distance";
        SetSpeed(velocity, 0);
        checkActive = true;
    }

    public void DriveTurn(float rotation, float velocity)
    {
        resetController();
        targetRot = rotation;
        checkType = "rotation";
        SetSpeed(0, velocity);
        checkActive = true;
    }

    public void DriveCurve(float distance, float rotation, float velocity)
    {
        resetController();
        targetDist = distance;
        checkType = "distance";
        SetSpeed(velocity, (float)velocity / distance * rotation); //finds w via ratio
        checkActive = true;
    }

    public int DriveDone()
    {
        if (!checkActive)
            return 1;

        //code for stalling or blockage, return 4

        return 0;
    }

    //set translational and rotational target velocities
    public void SetSpeed(float setv, float setw)
    {
        wheels[0].SetSpeed(setv + setw * wheelDist / 2 * Mathf.Deg2Rad);
        wheels[1].SetSpeed(setv - setw * wheelDist / 2 * Mathf.Deg2Rad);
    }

    private void updatePosition()
    {
        float lspeed = wheels[0].GetSpeed();
        float rspeed = wheels[1].GetSpeed();
        float newv = (rspeed + lspeed) / 2;
        float neww = (lspeed - rspeed) / wheelDist * Mathf.Rad2Deg;
        Pos.z += ((newv + v) / 2) * Mathf.Cos(Mathf.Deg2Rad * Rot);
        Pos.x += ((newv + v) / 2) * Mathf.Sin(Mathf.Deg2Rad * Rot);
        Rot += (neww + w) / 2;

        while (Rot > 180)
        {
            Rot -= 360;
        }
        while (Rot < -180)
        {
            Rot += 360;
        }

        v = newv;
        w = neww;
    }

    private void resetController()
    {
        targetDist = 0;
        targetRot = 0;
        travelledDist = 0;
        travelledRot = 0;
    }

    public void TestDelegation()
    {
        if (DriveDoneDelegate != null)
        {
            DriveDoneDelegate();
        }
    }

    private void checkDrive()
    {
        if (!checkActive)
            return;

        switch (checkType)
        {
            case "distance":
                travelledDist += v;
                if (Mathf.Sign(targetDist) * (targetDist - travelledDist) > 0)
                    return;
                break;
            case "rotation":
                travelledRot += w;
                if (Mathf.Sign(targetRot) * (targetRot - travelledRot) > 0)
                    return;
                break;
            default:
                break;
        }

        //journey complete
        SetSpeed(0, 0);
        checkActive = false;
        resetController();
        Debug.Log("Drive is completed!");
        if (DriveDoneDelegate != null)
        {
            Debug.Log("Calling delegate chain");
            DriveDoneDelegate();
        }
    }
}