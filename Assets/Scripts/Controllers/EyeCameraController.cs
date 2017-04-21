using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotComponents;
using RobotCommands;

public class EyeCameraController : MonoBehaviour, ICameraControl {

    public List<EyeCamera> cameras;

	public EyeCameraController()
    {
        cameras = new List<EyeCamera>();
    }

	public byte[] GetBytes(int camera)
    {   
		return cameras [camera].GetBytes ();
    }
}
