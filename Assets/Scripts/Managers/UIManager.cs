using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager instance { get; private set; }

    public GameObject fileFinderPrefab;

    public bool windowOpen = false;

    private WorldBuilder worldBuilder;
    private RobotBuilder robotBuilder;

    private FileFinder worldFileFinder;
    private FileFinder robotFileFinder;

    private void CreateButtons()
    {
        GameObject worldObj = Instantiate<GameObject>(fileFinderPrefab);
        worldObj.GetComponent<FileFinder>().Initialise("*.wld", "Load World", worldBuilder, 10, 10);
        worldObj.name = "WorldFileFinder";
        worldObj.transform.SetParent(GameObject.Find("Canvas").transform);

        GameObject robotObj = Instantiate<GameObject>(fileFinderPrefab);
        robotFileFinder = robotObj.GetComponent<FileFinder>().Initialise("*.robi", "Load Robot", robotBuilder, 120, 10);
        robotObj.name = "RobotFilefinder";
        robotObj.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    // Enforce singleton
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this);
    }

    void Start()
    {
        worldBuilder = WorldBuilder.instance;
        robotBuilder = RobotBuilder.instance;
        fileFinderPrefab = Resources.Load("FileFinderPrefab") as GameObject;
        CreateButtons();    
    }
}
