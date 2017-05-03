using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// Object manager handles objects in the scene
// Allows placement of objects at run-time
public class ObjectManager : MonoBehaviour {

    public static ObjectManager instance = null;

    // Stores reference to the Ground LayerMask, and shaders to use during placement
    public LayerMask groundMask;
    public Material validMat;
    public Material invalidMat;

    public GameObject placeableCylinder;
    public GameObject placeableCube;

    // Specific object currently being placed (one at a time strict)
    public bool objectBeingPlaced = false;
    public PlaceableObject objectOnMouse;

    private void Awake()
    {
        if (instance == null || instance == this)
        {
            instance = this;
        } 
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    public void AddTestObject()
    {
        GameObject testBot = Resources.Load("TestRobot") as GameObject;
        objectOnMouse = Instantiate(testBot).GetComponent<PlaceableObject>();
        objectBeingPlaced = true;
    }

    public void AddCylinderToScene()
    {
        PlaceableObject newCyl = Instantiate(placeableCylinder).GetComponent<PlaceableObject>();
        AddObjectToMouse(newCyl);
    }

    public void AddObjectToMouse(PlaceableObject newObject)
    {
        objectOnMouse = newObject;
        objectBeingPlaced = true;
        newObject.AttachToMouse();
    }

    public void PlaceObject()
    {
        objectBeingPlaced = false;
        objectOnMouse.PlaceObject();
        objectOnMouse = null;
    }

    private void Update()
    {
        if (objectBeingPlaced)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000f, groundMask))
            {
                objectOnMouse.transform.position = new Vector3(hit.point.x, 0.03f, hit.point.z);
            }
            if(Input.GetKeyDown(KeyCode.A) && objectOnMouse.validPlacement)
            {
                PlaceObject();
            }
        }
    }
}
