using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour {

    public static SimManager instance { get; private set; }
   

	public ServerManager server;
	public Dictionary<string,Robot> robots;
	HashSet<string> IDs;
	public GameObject world;

    // Enforce singleton
    public void Awake() 
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    // Use this for initialization
    public void Start() {
        server = gameObject.GetComponent<ServerManager>();

		robots = new Dictionary<string,Robot> ();
		IDs = new HashSet<string> ();
	}

	public Robot GetRobot(string id) {
		return robots [id];
	}

	// Update is called once per frame
	void Update () {
		server.Update ();
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
