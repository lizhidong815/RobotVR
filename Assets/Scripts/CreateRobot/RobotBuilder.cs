using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBuilder: MonoBehaviour, IFileReceiver{

    public static RobotBuilder instance = null;

	public string filepath;
    private RobotFactory factory = null;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    public GameObject ReceiveFile(string filepath)
    {
        this.filepath = filepath;
        IO io = new IO();
        if (!io.Load(filepath))
            return null;
        if (factory != null)
        {
            Debug.Log("Robot creation already exists?");
            return null;
        }

        factory = new RobotFactory();
        string line;

        while ( (line = io.readLine()) != "ENDOFFILE") {
			if (line.Length > 0) {
				if (line [0] != '#') {
					process (line);
				}
			}
		}
		return factory.robotObject;
	}

	public void process(string line){
        Debug.Log(line);
		string[] args = line.Split (' ');
		switch (args[0]) {
            case "name":
                SetType(args);
                break;
		    case "model":
			    addModel(args);
			    break;
            case "axis":
                addAxel(args);
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

    void SetType(string[] args)
    {
        factory.RobotType(args[1]);
    }

	void addModel(string[] args)
    {
		string modelpath = filepath.Substring (0, filepath.LastIndexOf ('\\')) + "\\" + args [1];
        factory.AddModel(modelpath);
	}

    void addAxel(string[] args)
    {
        factory.AddAxel(float.Parse(args[1])/1000, 0);
    }

	void addPSD(string[] args)
    {	
	    Vector3 psdPos = new Vector3 (-1*float.Parse(args[4])/1000,float.Parse(args[5])/1000,float.Parse(args[3])/1000);
		Quaternion psdRot = Quaternion.Euler (0, -1*float.Parse(args[6]), 0);
        factory.AddPSD(args[1], int.Parse(args[2]), psdPos, psdRot);
	}

	void addWheels(string[] args){
		factory.AddWheels(float.Parse (args [1]) / 1000f, float.Parse(args[2])/1000, int.Parse(args[3]), float.Parse (args [4]) / 1000f, 0);
	}
}
