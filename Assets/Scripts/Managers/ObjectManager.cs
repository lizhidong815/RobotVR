using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// Object in the scene that can be placed with the ObjectManager
// Robots, cans, cubes, etc.
public abstract class PlaceableObject : MonoBehaviour
{
    public bool validPlacement = true;
    public bool isPlaced = false;

    protected Rigidbody rigidBody;
    protected Collider objCollider;

    [SerializeField]
    private Renderer objRenderer;
    [SerializeField]
    private Material defaultMat;
    private Material validMat;
    private Material invalidMat;

    private void Start()
    {
        objRenderer = gameObject.transform.Find("Model").GetChild(0).GetComponent<MeshRenderer>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        objCollider = gameObject.GetComponent<Collider>();
        defaultMat = objRenderer.material;
        validMat = ObjectManager.instance.validMat;
        invalidMat = ObjectManager.instance.invalidMat;

        objRenderer.material = validMat;
        objCollider.isTrigger = true;
        rigidBody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced)
        {
            objRenderer.material = invalidMat;
            validPlacement = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isPlaced)
        {
            objRenderer.material = validMat;
            validPlacement = true;
        }
    }

    public void PlaceObject()
    {
        objRenderer.material = defaultMat;
        objCollider.isTrigger = false;
        rigidBody.isKinematic = false;
        isPlaced = true;
    }
}

// Object manager handles objects in the scene
// Allows placement of objects at run-time
public class ObjectManager : MonoBehaviour {

    public static ObjectManager instance = null;

    // Stores reference to the Ground LayerMask, and shaders to use during placement
    public LayerMask groundMask;
    public Material validMat;
    public Material invalidMat;

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
        groundMask = LayerMask.GetMask("Ground");
        validMat = (Material)Resources.Load("Materials/PlacementValidMaterial", typeof(Material));
        invalidMat = (Material)Resources.Load("Materials/PlacementInvalidMaterial", typeof(Material));
    }

    public void AddTestObject()
    {
        GameObject testBot = Resources.Load("TestRobot") as GameObject;
        objectOnMouse = Instantiate(testBot).GetComponent<PlaceableObject>();
        objectBeingPlaced = true;
    }

    public void AddCylinderToScene()
    {
        GameObject newCylinder = Resources.Load("PlaceableCylinder") as GameObject;
        PlaceableObject newCyl = Instantiate(newCylinder).GetComponent<PlaceableObject>();
        AddObjectToMouse(newCyl);
    }

    public void AddObjectToMouse(PlaceableObject newObject)
    {
        objectOnMouse = newObject;
        objectBeingPlaced = true;
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
                objectOnMouse.transform.position = new Vector3(hit.point.x, 0.02f, hit.point.z);
            }
            if(Input.GetKeyDown(KeyCode.A) && objectOnMouse.validPlacement)
            {
                PlaceObject();
            }
        }
    }
}
