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
    
public interface IVWUsable
{
    void InitalizeVW(int[] args);
    void StopVWControl();
    void SetVWParams(int[] args);
}

public interface IVWDrivable : IVWUsable
{
    void VWSetVehicleSpeed(int[] args);
    Speed VWGetVehicleSpeed();
    void VWDriveStraight(int[] args);
    void VWDriveTurn(int[] args);
    void VwDriveCurve(int[] args);
    int VWDriveRemaining();
    bool VWDriveDone();
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

// Abstract robot
// Universal functions
public abstract class Robot : MonoBehaviour {


}
