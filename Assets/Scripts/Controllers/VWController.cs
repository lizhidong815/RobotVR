using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotCommands;

public class VWController : MonoBehaviour, IVWDriveControl
{
    VWParams vwParams;

    public VWController()
    {
        vwParams = new VWParams();
    }

    public void Initialize(int tick, int bs, int max, int dir)
    {
        throw new NotImplementedException();
    }

    public void SetParameters(int vv, int tv, int vw, int tw)
    {
        vwParams.vv = vv;
        vwParams.tv = tv;
        vwParams.vw = vw;
        vwParams.tw = tw;
    }

    public void StopControl()
    {
        throw new NotImplementedException();
    }

    public void SetSpeed(int linear, int angular)
    {
        throw new NotImplementedException();
    }

    public Speed GetSpeed()
    {
        throw new NotImplementedException();
    }

    public void DriveStraight(int speed, int distance)
    {
        throw new NotImplementedException();
    }

    public void DriveTurn(int rotSpeed, int angle)
    {
        throw new NotImplementedException();
    }

    public void DriveCurve(int speed, int distance, int angle)
    {
        throw new NotImplementedException();
    }

    public int DriveRemaining()
    {
        throw new NotImplementedException();
    }

    public bool DriveDone()
    {
        throw new NotImplementedException();
    }
}

public class VWParams
{
    public int vv = 0;
    public int tv = 0;
    public int vw = 0;
    public int tw = 0;
}

public class Speed
{
    public int linear;
    public int angular;
}