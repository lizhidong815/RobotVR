using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotCommands;

public class PoseController : MonoBehaviour {

    private Pose pose;

    public PoseController()
    {
        pose = new Pose(0, 0, 0);
    }

    public Pose GetVehiclePose()
    {
        return pose;
    }

    public void SetVehiclePose(int x, int y, int phi)
    {
        Debug.Log("Setting Pose: " + x + " " + y + " " + phi);
        pose.x = x;
        pose.y = y;
        pose.phi = phi;
    }
}