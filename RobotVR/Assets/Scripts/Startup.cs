using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

	SimManager simManager;

	// Use this for initialization
	void Start () {
		BuildManager ();
		BuildServer ();
		//LoadRobots ();
		ReadCommands ();
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void BuildManager () {
		GameObject manager = Instantiate (Resources.Load ("Empty")) as GameObject;
		simManager = manager.AddComponent<SimManager> ();
		simManager.Start ();
		manager.name = "SimManager";
		ServerManager server = new ServerManager ();
		server.simManager = simManager;
		simManager.server = server;
	}

	void BuildServer () {
		
	}

	void LoadRobots () {
		gameObject.AddComponent<RobotLoader> ().LoadRobots ();

	}

	void ReadCommands () {
		string[] args = System.Environment.GetCommandLineArgs ();
		for (int i = 0; i < args.Length; i++) {
			Debug.Log ("ARG " + i + ": " + args [i]);
			//if (args [i] == "-input") {
				SimReader sr = gameObject.AddComponent<SimReader> ();
				sr.simManager = simManager;
				sr.read ("/Users/JoelFrewin/Documents/RobotVR/SPEED.sim");//args [i + 1]);
			//}
		}
	}
}
