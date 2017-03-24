using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBuilder: MonoBehaviour{

	public GameObject robot;

	public GameObject readRobi (string filepath) {
		robot = Instantiate (Resources.Load ("Empty")) as GameObject;
		IO io = new IO();
		if (!io.Load (filepath))
			return null;
		while (true) {
			string line = io.readLine ();
			if (line == "ENDOFFILE")
				break;
			//make sure line isnt blank
			if (line.Length > 0) {
				//check for hashtag
				if (line [0] != '#') {
					//read line below
					process (line);
				}
			}
		}
		return robot;
	}

	public void process(string line){
		
		string[] args = line.Split (' ');
		switch (args[0]) {
		case "model":
			addModel(args);
			break;
		default:
			break;
		}
	}

	void addModel(string[] arguments){
		foreach (string s in arguments) {
			print (s);
		}
	}


}
