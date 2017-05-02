using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public delegate void CollisionHandler();

public interface IPlaceable
{
    Action EnterPlacementCollision { get; set; }
    CollisionHandler ExitPlacementCollision { get; set; }
}

public abstract class PlaceableObject : MonoBehaviour
{

}

public class ObjectManager : MonoBehaviour {

    public static ObjectManager instance = null;

    public LayerMask groundMask;
    public Material validMat;
    public Material invalidMat;
    public Material defaultMat;

    public bool objectBeingPlaced = false;
    public GameObject objectOnMouse;
    private Renderer objectRenderer;
    private bool validPlacement = true;

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
        validMat = (Material)Resources.Load("Materials/DefaultMaterial", typeof(Material));
        validMat = (Material)Resources.Load("Materials/PlacementValidMaterial", typeof(Material));
        invalidMat = (Material)Resources.Load("Materials/PlacementInvalidMaterial", typeof(Material));
    }

    public void AddObjectToMouse(GameObject newObject)
    {
        IPlaceable placeable = newObject.GetComponent<IPlaceable>();
        if(placeable == null)
        {
            Debug.Log("Something went wrong in AddObjectToMouse : Placeable invalid");
        }

        objectOnMouse = newObject;
        objectBeingPlaced = true;
        objectRenderer = objectOnMouse.GetComponent<MeshRenderer>();
        
        placeable.EnterPlacementCollision = PlacementCollisionEnter;
        placeable.ExitPlacementCollision = PlacementCollisionExit;
    }

    /*
    public void AddRobotToMouse(Robot newRobot)
    {
        newRobot.EnterPlacementCollision = PlacementCollisionEnter;
        newRobot.ExitPlacementCollision = PlacementCollisionExit;
    }

    public void AddObjectToMouse(GameObject newObject)
    {
        objectOnMouse = newObject;
        objectBeingPlaced = true;
        objectRenderer = objectOnMouse.GetComponent<MeshRenderer>();
    }
    */

    public void PlacementCollisionEnter()
    {
        objectRenderer.material = invalidMat;
        validPlacement = false;
    }

    public void PlacementCollisionExit()
    {
        objectRenderer.material = validMat;
        validPlacement = true;
    }

    public void PlaceObject()
    {
        Debug.Log("Hi there.");
    }

    private void Update()
    {
        if (objectBeingPlaced)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000f, groundMask))
            {
                objectOnMouse.transform.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
            }
            if(Input.GetKeyDown(KeyCode.A) && validPlacement)
            {
                PlaceObject();
            }
        }
    }
    /* 
    WORKING ON THIS STILL coming soon

    protected virtual void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        myCollider = gameObject.GetComponent<BoxCollider>();

        valid = ObjectManager.instance.validMat;
        invalid = ObjectManager.instance.invalidMat;
        myMat = ObjectManager.instance.defaultMat;
        groundMask = ObjectManager.instance.groundMask;

        myBody.isKinematic = true;
        myCollider.isTrigger = true;
    }

    protected virtual void Update()
    {

        if (beingPlaced == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, groundMask))
            {
                transform.position = new Vector3(hit.point.x, 0.2f, hit.point.z);
            }

            // Check of the placement condition is met
            if (Input.GetKeyDown(KeyCode.A) && validPlacement)
            {
                myBody.isKinematic = false;
                myCollider.isTrigger = false;
                modelRenderer.material = myMat;
                beingPlaced = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (beingPlaced)
        {
            validPlacement = false;
            modelRenderer.material = invalid;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (beingPlaced)
        {
            validPlacement = true;
            modelRenderer.material = valid;
        }
    }
    */

}
