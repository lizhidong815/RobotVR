using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPosable
{
    void SetPose(int[] args);
    Pose GetPose();
}

public interface IMotors
{
    void DriveMotor(int[] args);
}

public interface IPIDUsable
{
    void DriveMotorControlled(int[] args);
    void SetPID(int[] args);
}
    
public interface IVWDrivable
{
    void InitalizeVW(int[] args);
    void VWSetVehicleSpeed(int[] args);
    Speed VWGetVehicleSpeed();
    void VWDriveStraight(int[] args);
    void VWDriveTurn(int[] args);
    void VWDriveCurve(int[] args);
    int VWDriveRemaining();
    int VWDriveDone();
    void VWDriveWait(Action<RobotConnection> doneCallback);
}

public interface IServoSettable
{
    void SetServo(int[] args);
}

public interface IPSDSensors
{
    UInt16 GetPSD(int args);
}

public interface HasCameras
{
    byte[] GetCameraOutput(int camera);
    void SetCameraResolution(int[] args);
}

public class Speed
{
    public int linear;
    public int angular;
}
// Abstract robot
// Universal functions
public abstract class Robot : PlaceableObject {

    public int axels = 0;
    public RobotConnection myConnection;

}
