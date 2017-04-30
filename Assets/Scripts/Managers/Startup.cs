using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

    public static bool doneStartup = false;

    GameObject test;

    private void Awake()
    {
        if (doneStartup)
            Destroy(this);
    }

    // Use this for initialization
    void Start () {
        gameObject.AddComponent<SimManager>();
        gameObject.AddComponent<ServerManager>();
        gameObject.AddComponent<RobotBuilder>();
        gameObject.AddComponent <WorldBuilder>();
        gameObject.AddComponent<UIManager>();
        doneStartup = true;
        Destroy (this);
	}

	void BuildManager () {

	}

	void BuildServer () {
		
	}

	void LoadRobots () {

	}

    /*
	void ReadCommands () {
		string[] args = System.Environment.GetCommandLineArgs ();
		for (int i = 0; i < args.Length; i++) {
			Debug.Log ("ARG " + i + ": " + args [i]);
			//if (args [i] == "-input") {
				SimReader sr = gameObject.AddComponent<SimReader> ();
				sr.simManager = simManager;
				sr.read (ApplicationHelper.localDataPath() + "/examples/SPEED.sim");//args [i + 1]);
			//}
		}
	}
    */
}
