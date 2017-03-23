using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (Application.dataPath);
		LoadRobots robotload = new LoadRobots ();
		robotload.buildRobots ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
