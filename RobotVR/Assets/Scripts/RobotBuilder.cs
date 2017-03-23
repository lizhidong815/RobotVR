using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBuilder : MonoBehaviour {

	public void buildRobot (string filename) {
		IO io = new IO();
		if (!io.Load ("Robots/" + filename))
			return;
		while (true) {
			string line = io.readLine ();
			if (line == "ENDOFFILE")
				break;
			//make sure line isnt blank
			if (line.Length > 0) {
				//check for hashtag
				if (line [0] == '#') {
					//read line below
					string parameters = io.readLine ();
					if (parameters == "ENDOFFILE")
						break;
					process (line, parameters);
				}
			}
		}
	}

	public void process(string line, string parameters){
		switch (line) {
		case "#body":
			buildSquare(parameters);
			break;
		default:
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void buildSquare(string parameters){
		string[] sizestring = parameters.Split (' ');
		float[] size = new float[3];
		for (int i = 0; i < 3; i++) {
			size [i] = float.Parse (sizestring [i]);
		}
		GameObject body = Instantiate (Resources.Load ("Body")) as GameObject;
		//body.transform.SetParent (robotObject.transform, false);
		body.transform.localScale = new Vector3 (size [0], size [1], size [2]);
	}
}
