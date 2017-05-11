using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour, IFileReceiver {

    public static WorldBuilder instance = null;

    public GameObject world;
	public string filepath;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    public GameObject ReceiveFile(string filepath)
    {
		GameObject.DestroyImmediate (GameObject.Find ("World"));
		this.filepath = filepath;
        world = new GameObject();
		world.name = "World";
		IO io = new IO();
        string line;

        if (!io.Load (filepath))
			return null;
		while ((line = io.readLine()) != "ENDOFFILE") {
			if (line.Length > 0) {
				if (line [0] != '#') {
					process (line);
				}
			}
		}
		return world;
	}

	public void process (string line){

		string[] args = line.Split (' ');
		switch (args[0]) {
		case "floor":
			addFloor(args);
			break;
		default:
			addWall (args); 
			break;
		}
	}

	void addWall (string[] args) {
		GameObject wall = Instantiate(Resources.Load("Cube")) as GameObject;
		wall.name = "wall";
		Vector2 start = new Vector2(float.Parse(args[0])/1000, float.Parse(args[1])/1000);
		Vector2 end = new Vector2(float.Parse(args[2])/1000, float.Parse(args[3])/1000);
		wall.transform.localScale = new Vector3 (Vector2.Distance(start, end),0.1f,0.01f);
		wall.transform.position = new Vector3 ((end.x+start.x)/2,0.05f,(end.y+start.y)/2);
		wall.transform.rotation = Quaternion.Euler (0,Mathf.Atan2(end.y-start.y,end.x-start.x)/Mathf.PI*180,0);
		wall.transform.SetParent (world.transform);
	}

	void addFloor (string[] args) {
		GameObject floor = Instantiate(Resources.Load("Cube")) as GameObject;
		floor.name = "floor";
		floor.transform.localScale = new Vector3 (float.Parse(args[1])/1000,0.1f,float.Parse(args[2])/1000);
		floor.transform.position = new Vector3 (float.Parse(args[1])/2000,-0.05f,float.Parse(args[2])/2000);
		floor.transform.SetParent (world.transform);
	}
}
