using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour {

    public static SimManager instance = null;

	public ServerManager server;
	public Dictionary<string,Robot> robots;
	HashSet<string> IDs;
	public GameObject world;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    private void Start() {
		robots = new Dictionary<string,Robot> ();
		IDs = new HashSet<string> ();
	}

	public Robot GetRobot(string id) {
		return robots [id];
	}

	public string newID() {
		string myString;
		do {
			string glyphs = "abcdefghijklmnopqrstuvwxyz";
			myString = "";
			for (int i = 0; i < 8; i++) {
				myString += glyphs [Random.Range (0, glyphs.Length)];
			}
		} while (IDs.Contains (myString));
		IDs.Add (myString);
		return myString;
	}
}
