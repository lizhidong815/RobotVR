using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLoader: MonoBehaviour{

	public List<GameObject> robottypes;

	// Use this for initialization
	void Start () {
		
	}

	public void LoadRobots () {
		IO io = new IO();
		List<string> filenames = io.getFileNames ("Robots", "*.robi");
		filenames.Remove(".DS_Store");
		RobotBuilder rb = gameObject.AddComponent<RobotBuilder> ();
		foreach (string filename in filenames) {
			Debug.Log ("building " + filename);
			robottypes.Add(rb.readRobi (filename));
		}
	}



}
