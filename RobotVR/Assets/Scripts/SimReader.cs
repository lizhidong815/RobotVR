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
			string id = simManager.newID ();
			simManager.server.pendingconns.Enqueue (id);
			/*build robot*/
			RobotBuilder rb = gameObject.AddComponent<RobotBuilder> ();
			Robot robot = rb.readRobi (ApplicationHelper.localDataPath() + args [1]);
			robot.transform.position = new Vector3 (float.Parse(args[3])/1000,0,float.Parse(args[4])/1000);
			robot.id = id;
			robot.name = id;
			simManager.robots.Add(id, robot);
			/*run client*/

			break;
		default:
			break;
		}
	}

}
