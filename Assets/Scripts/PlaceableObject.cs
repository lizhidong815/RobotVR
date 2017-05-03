using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Object in the scene that can be placed with the ObjectManager
// Robots, cans, cubes, etc.
public abstract class PlaceableObject : MonoBehaviour, IPointerClickHandler
{
    public bool validPlacement = true;
    public bool isPlaced = false;
    public bool isSelected = false;
    public int currLayer = 0;

    public int objectID;

    protected Rigidbody rigidBody;
    protected Collider objCollider;

    private GameObject modelContainer;
    [SerializeField]
    private List<Material> defaultMats;
    private Material validMat;
    private Material invalidMat;

    private void Awake()
    {
        modelContainer = transform.Find("Model").gameObject;
        rigidBody = gameObject.GetComponent<Rigidbody>();
        objCollider = gameObject.GetComponent<Collider>();
        defaultMats = new List<Material>();
        validMat = ObjectManager.instance.validMat;
        invalidMat = ObjectManager.instance.invalidMat;
        foreach (Transform child in modelContainer.transform)
        {
            Renderer childRend = child.GetComponent<Renderer>();
            defaultMats.Add(childRend.material);
        }
    }

    public void AttachToMouse()
    {
        foreach (Transform child in modelContainer.transform)
        {
            child.GetComponent<Renderer>().material = validMat;
        }
        validPlacement = true;
        objCollider.isTrigger = true;
        rigidBody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced)
        {
            foreach (Transform child in modelContainer.transform)
            {
                child.GetComponent<Renderer>().material = invalidMat;           
            }
            validPlacement = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isPlaced)
        {
            foreach (Transform child in modelContainer.transform)
            {
                child.GetComponent<Renderer>().material = validMat;
            }
            validPlacement = true;
        }
    }

    protected void Select()
    {
        if (isPlaced)
        {
            isSelected = !isSelected;
            currLayer = isSelected ? 11 : 0;
            SetHighlightLayer(gameObject, currLayer);
        }
    }

    public void SetHighlightLayer(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetHighlightLayer(child.gameObject, layer);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    public void PlaceObject()
    {
        int i = 0;
        foreach (Transform child in modelContainer.transform)
        {
            child.GetComponent<Renderer>().material = defaultMats[i];
            i++;
        }
        objCollider.isTrigger = false;
        rigidBody.isKinematic = false;
        isPlaced = true;
    }
}
