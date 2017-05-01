using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public static ObjectManager instance = null;

    public LayerMask groundMask;

    public Material validMat;
    public Material invalidMat;
    public Material defaultMat;

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
    /* 
    WORKING ON THIS STILL coming soon


    public int axels = 0;
    public bool beingPlaced = true;

    public Renderer modelRenderer;

    protected Material myMat;
    private Rigidbody myBody;
    private Collider myCollider;
    private LayerMask groundMask;

    private Material valid;
    private Material invalid;
    [SerializeField]
    private bool validPlacement = true;

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
