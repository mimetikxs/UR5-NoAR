using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is intended for debugging only
// Helps visualize and set the positions and orientations of the hotspots' transform

[ExecuteInEditMode]
public class LookAtLightTarget : MonoBehaviour 
{
	public bool lightEnabled = false;
	private bool wasLightEnabled = true;

	public bool lookAtEnabled = true;

	private Transform target;
	private GameObject debugLight;


	void Awake()
	{
		target = transform.parent.Find ("LightTarget");	
		debugLight = transform.Find ("SpotlightAux").gameObject;
	}


	void Start () 
	{
	}
	

	void Update () 
	{
		// only act when dirty
		if (lightEnabled != wasLightEnabled) 
		{
			debugLight.SetActive (lightEnabled);
			wasLightEnabled = lightEnabled;
		}

		if (lookAtEnabled) 
		{
			transform.LookAt (target.position);
			transform.Rotate (90f, 0f, 0f);
		}
	}


	public void EnableLight(bool val)
	{
		lightEnabled = val;
	}


	public void EnableLookAt(bool val)
	{
		lookAtEnabled = val;

		//target.gameObject.SetActive (val);
	}
}
