using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is intended for debugging only
// Helps visualize and set the positions and orientations of the hotspots' transform

[ExecuteInEditMode]
public class LookAtLightTarget : MonoBehaviour 
{
	public bool lightEnabled = false;
	public bool lookAtEnabled = true;
	public bool drawDirection = true;

	private Transform target;
	private GameObject debugLight;


	void Awake()
	{
		target = transform.parent.Find ("LightTarget");	
		debugLight = transform.Find ("SpotlightAux").gameObject;
	}


	void Start () 
	{
		debugLight.SetActive (lightEnabled);
	}
	

	void Update () 
	{
		if (lookAtEnabled) 
		{
			transform.LookAt (target.position);
			transform.Rotate (90f, 0f, 0f);
		}

		if (drawDirection) 
		{
			Debug.DrawLine (
				debugLight.transform.position,
				target.position,
				Color.yellow
			);
		}
	}


	void OnValidate()
	{
		if (debugLight)
			debugLight.SetActive (lightEnabled);
	}


	public void EnableLight(bool val)
	{
		lightEnabled = val;

		if (debugLight)
			debugLight.SetActive (lightEnabled);
	}


	public void EnableLookAt(bool val)
	{
		lookAtEnabled = val;
	}


	public void EnableDrawLine(bool val)
	{
		drawDirection = val;
	}
}
