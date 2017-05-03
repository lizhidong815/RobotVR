using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabBot : Robot, 
    IMotors,
    IPIDUsable,
    IPosable,
    IPSDSensors,
    IServoSettable,
    IVWDrivable,
    HasCameras
{
    // Controllers
    public WheelMotorController wheelController;
    public PoseController poseController;
    public PSDController psdController;
    public ServoController servoController;
    public EyeCameraController eyeCamController;

    Action<RobotConnection> driveDoneDelegate;

    // Initialize controllers
    public LabBot()
    { 

    }

    // Initialize commands
    private void Awake()
    {
        wheelController = gameObject.AddComponent<WheelMotorController>();
        poseController = gameObject.AddComponent<PoseController>();
        psdController = gameObject.AddComponent<PSDController>();
        servoController = gameObject.AddComponent<ServoController>();
        eyeCamController = gameObject.AddComponent<EyeCameraController>();
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

    public void VWSetVehicleSpeed(int[] args)
    {
        throw new NotImplementedException();
    }

    public Speed VWGetVehicleSpeed()
    {
        throw new NotImplementedException();
    }

    public void VWDriveStraight(int distance, int speed)
    {
		wheelController.DriveStraight ((float) distance/1000, (float) speed/1000);
    }

    public void VWDriveTurn(int[] args)
    {
		wheelController.DriveTurn (args [0], args [1]);
    }

    public void VWDriveCurve(int[] args)
    {
		wheelController.DriveCurve ((float) args [0]/1000, args [1], (float) args [2]/1000);
    }

    public int VWDriveRemaining()
    {
        throw new NotImplementedException();
    }

    public int VWDriveDone()
    {
		return wheelController.DriveDone ();
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