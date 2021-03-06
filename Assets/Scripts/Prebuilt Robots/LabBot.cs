﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotComponents;

public class LabBot : Robot, 
    IMotors,
    IPIDUsable,
    IPosable,
    IPSDSensors,
    IServoSettable,
    IVWDrivable,
    ICameras
{
    // Controllers
    public WheelMotorController wheelController;
    public PSDController psdController;
    public ServoController servoController;
    public EyeCameraController eyeCamController;

    Action<RobotConnection> driveDoneDelegate;

    // Initialize commands
    private void Awake()
    {

    }

    // This function sets the controllers for a newly created LabBot object
    // Used when a robot is created from file
    public void Initialize()
    {
        wheelController = gameObject.AddComponent<WheelMotorController>();
        psdController = gameObject.AddComponent<PSDController>();
        servoController = gameObject.AddComponent<ServoController>();
        eyeCamController = gameObject.AddComponent<EyeCameraController>();
        wheelController.wheels = new List<Wheel>();
        psdController.sensors = new List<PSDSensor>();
        servoController.servos = new List<Servo>();
        eyeCamController.cameras = new List<EyeCamera>();
    }

    public void DriveDoneCallback()
    {
        driveDoneDelegate(myConnection);
    }

    public void DriveMotor(int motor, int speed)
    {
        wheelController.SetMotorSpeed(motor, speed);
    }

    public void DriveMotorControlled(int motor, int ticks)
    {
        wheelController.SetMotorControlled(motor, ticks);
    }

    public void SetPID(int motor, int p, int i, int d)
    {
        wheelController.SetPIDParams(motor, p, i, d);
    }

    public void SetServo(int servo, int angle)
    {
        servoController.SetServoPosition(servo, angle);
    }

    public void SetPose(int x, int y, int phi)
    {
        wheelController.Pos.x = x;
        wheelController.Pos.z = y;
        wheelController.Rot = phi;
    }

    public Pose GetPose()
    {
        return new Pose(Convert.ToInt32(Math.Round(wheelController.Pos.x)), 
            Convert.ToInt32(Math.Round(wheelController.Pos.z)), 
            Convert.ToInt32(Math.Round(wheelController.Rot)));
    }

    public UInt16 GetPSD(int psd)
    {
        return psdController.GetPSDValue(psd);
    }

    public void VWSetVehicleSpeed(int linear, int angular)
    {
        throw new NotImplementedException();
    }

    public Speed VWGetVehicleSpeed()
    {
        return wheelController.GetSpeed();
    }

    public void VWDriveStraight(int distance, int speed)
    {
		wheelController.DriveStraight ((float) distance/1000, (float) speed/1000);
    }

    public void VWDriveTurn(int rotation, int velocity)
    {
        Debug.Log("Turn " + rotation + " degrees with speed " + velocity);
		wheelController.DriveTurn (rotation, velocity);
    }

    public void VWDriveCurve(int distance, int rotation, int velocity)
    {
		wheelController.DriveCurve ((float) distance/1000, rotation, (float) velocity/1000);
    }

    public int VWDriveRemaining()
    {
        return wheelController.DriveRemaining();
    }

    public bool VWDriveDone()
    {
		return wheelController.DriveDone ();
    }

    public int VWDriveStalled()
    {
        throw new NotImplementedException();
    }

    public void VWDriveWait(Action<RobotConnection> doneCallback)
    {
        driveDoneDelegate = doneCallback;
        wheelController.DriveDoneDelegate = DriveDoneCallback;
    }

    public void InitalizeVW(int[] args)
    {
        throw new NotImplementedException();
    }

    public byte[] GetCameraOutput(int camera)
    {
        return eyeCamController.GetBytes(camera);
    }

    public void SetCameraResolution(int camera, int width, int height)
    {
        eyeCamController.SetResolution(camera, width, height);
    }
}