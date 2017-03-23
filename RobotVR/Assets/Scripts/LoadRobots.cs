using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRobots{

	// Use this for initialization
	void Start () {
		
	}

	public void buildRobots () {
		IO io = new IO();
		List<string> filenames = io.getFileNames ("Robots", "*.robi");
		filenames.Remove(".DS_Store");
		foreach (string filename in filenames) {
			RobotBuilder robotbuilder = new RobotBuilder ();
			Debug.Log ("building " + filename);
			robotbuilder.buildRobot (filename);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
