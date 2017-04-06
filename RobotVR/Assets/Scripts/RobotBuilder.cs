using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBuilder: MonoBehaviour{

	public Robot robot;
	public string filepath;
	public Robot readRobi (string filepath) {
		this.filepath = filepath;
		GameObject robotobj = Instantiate (Resources.Load ("Empty")) as GameObject;
		robot = robotobj.AddComponent<Robot> ();
		robot.Start ();
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
		case "psd":
			addPSD(args);
			break;
		case "wheel":
			addWheels (args);
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
		GameObject model = OBJLoader.LoadOBJFile (modelpath);
		model.transform.SetParent(robot.transform, false);
		model.transform.rotation = Quaternion.Euler(0,90,0);
		BoxCollider boxcollider = model.AddComponent<BoxCollider> ();
		boxcollider.material = Resources.Load ("nofriction") as PhysicMaterial;
		boxcollider.center = new Vector3 (0, 0.1f, 0);
		boxcollider.size = new Vector3 (0.2f, 0.2f, 0.2f);
		model.AddComponent<Rigidbody> ();
		model.layer = 9;
	}

	void addPSD(string[] args){
		GameObject psdobj = Instantiate (Resources.Load ("Empty")) as GameObject;
		PSD psd = psdobj.AddComponent<PSD> ();
		psd.name = args [1];
		robot.psds.Add (psd);
		psd.transform.position = new Vector3 (-1*float.Parse(args[4])/1000,float.Parse(args[5])/1000,float.Parse(args[3])/1000);
		psd.transform.rotation = Quaternion.Euler (0, -1*float.Parse(args[6]), 0);
		psd.transform.SetParent (robot.transform, false);
	}

	void addWheels(string[] args){
		addWheel (float.Parse (args [1]) / 1000f, float.Parse (args [4]) / 1000f, -90f);
		addWheel (float.Parse (args [1]) / 1000f, float.Parse (args [4]) / -1000f, -90f);
	}

	void addWheel(float diameter, float distance, float rotation){
		GameObject wheel = Instantiate (Resources.Load("Cylinder")) as GameObject;
		wheel.transform.SetParent (robot.transform, false);
		wheel.transform.localScale = new Vector3 (diameter, 0.01f, diameter);
		wheel.transform.position = new Vector3 (distance, diameter/2, 0);
		wheel.transform.rotation = Quaternion.Euler (0, 0, rotation);
		wheel.layer = 8;
		HingeJoint hinge = wheel.AddComponent<HingeJoint> ();
		hinge.axis = new Vector3 (0, 1, 0);
		hinge.connectedBody = robot.transform.GetChild(0).GetComponent<Rigidbody>();
	}
}
