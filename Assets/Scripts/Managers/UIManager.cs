﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance { get; private set; }

    public GameObject fileFinderPrefab;

    public bool windowOpen = false;

    private WorldBuilder worldBuilder;
    private RobotBuilder robotBuilder;

    private FileFinder worldFileFinder;
    private FileFinder robotFileFinder;

	private Button loadworld;
	private Button loadrobot;
	private GameObject blockingPanel;
	private Transform robotList;
	private Transform clientList;

    private void CreateButtons()
    {
		Transform canvas = GameObject.Find ("Canvas").transform;
		robotList = canvas.GetChild(0).FindChild ("Robots");
		clientList = canvas.GetChild(0).FindChild ("Clients");
		blockingPanel = canvas.GetChild(0).FindChild("BlockingPanel").gameObject;

		Transform worldObj = canvas.GetChild(0).FindChild ("LoadWorld");
		worldFileFinder = worldObj.GetComponent<FileFinder>().Initialise("*.wld", "Load World", worldBuilder, 10, 10);
		worldObj.GetComponent<Button> ().onClick.AddListener (() => {worldFileFinder.OpenFileSelection();});
        worldObj.name = "WorldFileFinder";

		Transform robotObj = canvas.GetChild(0).FindChild ("LoadRobot");
        robotFileFinder = robotObj.GetComponent<FileFinder>().Initialise("*.robi", "Load Robot", robotBuilder, 120, 10);
		robotObj.GetComponent<Button> ().onClick.AddListener (() => {robotFileFinder.OpenFileSelection();});
        robotObj.name = "RobotFilefinder";
    }

    // Enforce singleton
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this);
    }

	public void openWindow(){
		windowOpen = true;
		blockingPanel.SetActive(true);
	}

	public void closeWindow(){
		windowOpen = false;
		blockingPanel.SetActive(false);
	}

    void Start()
    {
        worldBuilder = WorldBuilder.instance;
        robotBuilder = RobotBuilder.instance;
        fileFinderPrefab = Resources.Load("FileFinderPrefab") as GameObject;
        CreateButtons();    
    }
}
