using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

	public string id;
	public List<PSD> psds;

	// Use this for initialization
	public void Start () {
		psds = new List<PSD> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
