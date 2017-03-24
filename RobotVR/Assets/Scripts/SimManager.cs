using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour {

	public ServerManager server;
	public Dictionary<string,Robot> robots;
	// Use this for initialization

	public void Start() {
		robots = new Dictionary<string,Robot> ();
	}


	// Update is called once per frame
	void Update () {
		server.Update ();
	}


}
