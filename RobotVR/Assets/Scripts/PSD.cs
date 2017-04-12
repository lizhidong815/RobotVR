using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSD : MonoBehaviour {

	public float distance = float.MaxValue;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Debug.DrawRay (transform.position , transform.forward, Color.black);
		if(Physics.Raycast(transform.position , transform.forward, out hit))
		{
			if (hit.collider.gameObject.tag == "wall")
			{
				Debug.DrawRay (transform.position , transform.forward, Color.cyan);
				distance = hit.distance;
				return;
			}
		}
		distance = float.MaxValue;
	}
}
