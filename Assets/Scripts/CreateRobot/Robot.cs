using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPosable
{
    void SetPose(int x, int y, int phi);
    Pose GetPose();
}

public interface IMotors
{
    void DriveMotor(int motor, int speed);
}

public interface IPIDUsable
{
    void DriveMotorControlled(int motor, int ticks);
    void SetPID(int motor, int p, int i, int d);
}
    
public interface IVWDrivable
{
    void InitalizeVW(int[] args);
    void VWSetVehicleSpeed(int[] args);
    Speed VWGetVehicleSpeed();
    void VWDriveStraight(int distance, int speed);
    void VWDriveTurn(int rotation, int velocity);
    void VWDriveCurve(int distance, int rotation , int velocity);
    int VWDriveRemaining();
    int VWDriveDone();
    void VWDriveWait(Action<RobotConnection> doneCallback);
}

public interface IServoSettable
{
    void SetServo(int servo, int angle);
}

public interface IPSDSensors
{
    UInt16 GetPSD(int psd);
}

public interface HasCameras
{
    byte[] GetCameraOutput(int camera);
    void SetCameraResolution(int camera, int width, int height);
}

public class Speed
{
    public int linear;
    public int angular;
}
// Abstract robot
// Universal functions
public abstract class Robot : PlaceableObject
{
    public int axels = 0;
    public RobotConnection myConnection = null;
}
