using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BuildManagers ();
		ReadCommands ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void BuildManagers () {
		GameObject simManager = Instantiate (Resources.Load ("Empty")) as GameObject;
		simManager.name = "SimManager";
		SimManager sm = simManager.AddComponent<SimManager>( ) as SimManager;
		ServerManager server = new ServerManager ();
		sm.server = server;
		server.simManager = sm;
	}

	void ReadCommands () {
		string[] args = System.Environment.GetCommandLineArgs ();
		for (int i = 0; i < args.Length; i++) {
			Debug.Log ("ARG " + i + ": " + args [i]);
			if (args [i] == "-input") {
				SimReader.read (args [i + 1]);
			}
		}
	}
}
