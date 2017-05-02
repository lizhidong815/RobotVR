using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotCommands;

public class LabBot : Robot, 
    IMotors,
    IPIDUsable,
    IPosable,
    IPSDSensors,
    IServoSettable,
    IVWDrivable,
    HasCameras
{
    // Available Commands
    ICommand<int[]> driveMotor;
    ICommand<int[]> driveMotorControlled;
    ICommand<int[]> setPid;
    ICommand<int[]> setPose;
    ICommand<int[]> setServo;
    ICommand<int[]> setCamRes;

    // Returning commands need to have concrete type
    GetVehiclePoseCommand getPose;
    GetPSDSensorValueCommand getPsd;
    GetCameraOutputCommand getCamOut;

    // Controllers
    public WheelMotorController wheelController;
    public PoseController poseController;
    public PSDController psdController;
    public ServoController servoController;
    public EyeCameraController eyeCamController;

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

        driveMotor = new MotorDriveCommand(wheelController);
        driveMotorControlled = new MotorDriveControlledCommand(wheelController);
        setPid = new SetPIDCommand(wheelController);
        setPose = new SetVehiclePoseCommand(poseController);
        getPose = new GetVehiclePoseCommand(poseController);
        setServo = new SetServoCommand(servoController);
        getPsd = new GetPSDSensorValueCommand(psdController);
        getCamOut = new GetCameraOutputCommand(eyeCamController);
        setCamRes = new SetCameraResolutionCommand(eyeCamController);
    }


    public void DriveMotor(int[] args)
    {
        driveMotor.Execute(args);
    }

    public void DriveMotorControlled(int[] args)
    {
        driveMotorControlled.Execute(args);
    }

    public void SetPID(int[] args)
    {
        setPid.Execute(args);
    }

    public void SetServo(int[] args)
    {
        setServo.Execute(args);
    }
    public void SetPose(int[] args)
    {
        setPose.Execute(args);
    }

    public Pose GetPose()
    {
        getPose.Execute(0);
        return getPose._pose;
    }

    public UInt16 GetPSD(int args)
    {
        getPsd.Execute(args);
        return getPsd._value;
    }

    public void VWSetVehicleSpeed(int[] args)
    {
        throw new NotImplementedException();
    }

    public Speed VWGetVehicleSpeed()
    {
        throw new NotImplementedException();
    }

    public void VWDriveStraight(int[] args)
    {
		wheelController.DriveStraight ((float) args [0]/1000, (float) args [1]/1000);
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

    public void InitalizeVW(int[] args)
    {
        throw new NotImplementedException();
    }

    public void StopVWControl()
    {
        throw new NotImplementedException();
    }

    public void SetVWParams(int[] args)
    {
        throw new NotImplementedException();
    }

    public byte[] GetCameraOutput(int camera)
    {
        getCamOut.Execute(camera);
        return getCamOut.img;
    }

    public void SetCameraResolution(int[] args)
    {
        setCamRes.Execute(args);
    }
}