using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBuilder: MonoBehaviour{

	public GameObject robot;
	public string filepath;
	public GameObject readRobi (string filepath) {
		this.filepath = filepath;
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

	void addModel(string[] args){
		foreach (string s in args) {
			print (s);
		}
		string modelpath = filepath.Substring (0, filepath.LastIndexOf ('/')) + "/" + args [1];
		print (modelpath);
		OBJLoader.LoadOBJFile(modelpath).transform.SetParent(robot.transform, false);
	}


}
