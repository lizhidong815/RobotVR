using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotComponents;

public class RobotFactory {

    public GameObject robotObject;
    public Robot robot;

    public RobotFactory()
    {
        robotObject = new GameObject("GeneratedRobot");
        Rigidbody rb = robotObject.AddComponent<Rigidbody>();
        rb.mass = 1500;
        robot = null;
    }

    public void RobotType(string type)
    {
        switch (type)
        {
            case "LabBot":
                robot = robotObject.AddComponent<LabBot>();
                break;
            default:
                Debug.Log("Couldnt find robot type");
                break;
        }
    }

    public void AddModel(string modelPath)
    {
        GameObject model = OBJLoader.LoadOBJFile(modelPath);
        model.transform.SetParent(robotObject.transform, false);
        model.transform.rotation = Quaternion.Euler(0, 90, 0);

        BoxCollider robCollider = robotObject.AddComponent<BoxCollider>();
        robCollider.center = new Vector3(0, 0.1f, 0);
        robCollider.size = new Vector3(0.1f, 0.1f, 0.2f);
    }

    public void AddCube()
    {
        robot = robotObject.AddComponent<LabBot>();
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(robotObject.transform, false);
        cube.transform.localScale = new Vector3(1, 1, 3);
    }

    private Transform AddPSDContainer()
    {
        GameObject psdContainer = new GameObject("PSDContainer");
        psdContainer.transform.SetParent(robotObject.transform, false);
        return psdContainer.transform;
    }

    public void AddPSD(string name, int id, Vector3 pos, Quaternion rot)
    {
        if(!(robot is IPSDSensors))
        {
            Debug.Log("Trying to add PSD to unsupported robot type");
        }
        else
        {
            Transform psdContainer = robotObject.transform.Find("PSDContainer");
            if (psdContainer == null)
                psdContainer = AddPSDContainer();

            GameObject psd = new GameObject(name);
            psd.transform.SetParent(psdContainer, false);
            psd.transform.localPosition = pos;
            psd.transform.localRotation = rot;

            PSDSensor sensor = psd.AddComponent<PSDSensor>();
            (robot as LabBot).psdController.sensors.Add(sensor);
        }
    }

    private Transform AddWheelContainer()
    {
        GameObject wheelContainer = new GameObject("WheelContainer");
        wheelContainer.transform.SetParent(robotObject.transform, false);
        return wheelContainer.transform;
    }

    // Adds an axel (this is just a gameobject that represnets the 
    // z (forwards/backwars) and y (up/down) locaation
    public void AddAxel(float z, float y)
    {
        GameObject axel = new GameObject("Axel" + robot.axels);
        robot.axels++;
        axel.transform.SetParent(robotObject.transform, false);
        axel.transform.position = new Vector3(0, y, z);
    }

    // Add Wheels adds a pair of wheels on an axis
    public void AddWheels(float diameter, float vel, int enc, float wBase, int axel)
    {
        Transform axelTransform = robotObject.transform.FindChild("Axel" + axel);
        if(axelTransform == null)
        {
            Debug.Log("Trying to attach wheels to a non existand axels");
            return;
        }
        else
        {
            Object wheelPrefab = Resources.Load("WheelPrefab");
            GameObject wheelLeft = Object.Instantiate(wheelPrefab, axelTransform, false) as GameObject;
            wheelLeft.name = "leftwheel";
            GameObject wheelRight = Object.Instantiate(wheelPrefab, axelTransform, false) as GameObject;
            wheelRight.name = "rightwheel";

            wheelLeft.transform.SetParent(axelTransform, false);
            wheelRight.transform.SetParent(axelTransform, false);
            wheelLeft.transform.localScale = new Vector3(diameter, diameter, diameter);
            wheelRight.transform.localScale = new Vector3(diameter, diameter, diameter);
            wheelLeft.transform.localPosition = new Vector3(-wBase, 0, 0);
            wheelRight.transform.localPosition = new Vector3(wBase, 0, 0);

            WheelInfo wheelLeftInfo = new WheelInfo();
            wheelLeftInfo.wheel = wheelLeft.GetComponent<WheelCollider>();
            wheelLeftInfo.speed = vel / 1000;
            (robot as LabBot).wheelController.wheels.Add(wheelLeftInfo);

            WheelInfo wheelRightInfo = new WheelInfo();
            wheelRightInfo.wheel = wheelRight.GetComponent<WheelCollider>();
            wheelRightInfo.speed = vel / 1000;
            (robot as LabBot).wheelController.wheels.Add(wheelRightInfo);
        }
    }

}
