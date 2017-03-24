using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimReader {

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
		case "#robot":
			string[] args = parameters.Split (' ');
			string id = simManager.newID ();
			simManager.server.pendingconns.Enqueue (id);
			/*build robot*/
			/*run client*/

			break;
		default:
			break;
		}
	}

}
