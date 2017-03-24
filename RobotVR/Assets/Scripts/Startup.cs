using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

	SimManager simManager;

	// Use this for initialization
	void Start () {
		BuildManager ();
		BuildServer ();
		ReadCommands ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void BuildManager () {
		GameObject manager = Instantiate (Resources.Load ("Empty")) as GameObject;
		SimManager simManager = manager.AddComponent<SimManager> ();
		manager.name = "SimManager";
	}

	void BuildServer (){
		ServerManager server = new ServerManager ();
		server.simManager = simManager;
		simManager.server = server;
	}

	void ReadCommands () {
		string[] args = System.Environment.GetCommandLineArgs ();
		for (int i = 0; i < args.Length; i++) {
			Debug.Log ("ARG " + i + ": " + args [i]);
			if (args [i] == "-input") {
				SimReader sr = new SimReader ();
				sr.simManager = simManager;
				sr.read (args [i + 1]);
			}
		}
	}
}
