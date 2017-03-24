using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour {

	public ServerManager server;
	public Dictionary<string,Robot> robots;
	public Robot currentRobot;
	HashSet<string> IDs;

	// Use this for initialization

	public void Start() {
		robots = new Dictionary<string,Robot> ();
	}

	public void AddRobot(string id) {
		robots.Add (id, currentRobot);
		currentRobot.id = id;
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
