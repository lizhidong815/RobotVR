using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimReader {

	public static void read(string path) {
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

	public static void process(string line, string parameters){
		switch (line) {
		case "#body":
			
			break;
		default:
			break;
		}
	}

}
