using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour {

    public static SimManager instance = null;

	public ServerManager server;

    public List<PlaceableObject> allObjects;

    public GameObject world;

    private int totalObjects = 0;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    private void Start() {
        allObjects = new List<PlaceableObject>();
	}
    
    public PlaceableObject GetObjectByID(int id)
    {
        return allObjects.Find(x => x.objectID == id);
    }
}
