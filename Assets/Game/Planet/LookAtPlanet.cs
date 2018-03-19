using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlanet : MonoBehaviour {

	private Vector3 worldCenter = new Vector3(0f, 22.2f, -8f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (worldCenter);
	}
}
