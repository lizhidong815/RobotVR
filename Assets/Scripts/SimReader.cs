using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimReader: MonoBehaviour {

	public SimManager simManager;

	public void read(string path) {
		IO io = new IO ();
		if (!io.Load (path))
			return;
		while (true) {
			string line = io.readLine ();
			if (line == "ENDOFFILE")
				break;
			//make sure line isnt blank
			if (line.Length > 0) {
				//check for hashtag
				if (line [0] != '#') {
					process (line);
				}
			}
		}
	}

	public void process(string line){
		string[] args = line.Split (' ');
		print (args [0]);
		switch (args[0]) {
		case "robi":
			/*build robot*/
			RobotBuilder rb = gameObject.AddComponent<RobotBuilder> ();
			GameObject robot = rb.ReceiveFile (ApplicationHelper.localDataPath() + args [1]);
			robot.transform.position = new Vector3 (float.Parse(args[3])/1000,0,float.Parse(args[4])/1000);
			/*run client*/

			break;
		case "world":
			WorldBuilder wb = gameObject.AddComponent<WorldBuilder> ();
			simManager.world = wb.ReceiveFile (ApplicationHelper.localDataPath () + args [1]);
			simManager.world.name = "world";
			break;
		default:
			break;
		}
	}

}
