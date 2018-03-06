using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Handles logic to extinguish fires.
 * Controls the state of the virtual tool.
 * 
 * TODO: control position of hotspots
 */

public class RigShower : MonoBehaviour 
{
	//private Collider worldSphere;
	private Transform toolTarget;
	private Transform groundPlane;
	private Vector3 force;
	private WindTool windTool;

	private bool isOn;


	private void Awake()
	{
		windTool = transform.Find ("Tool").GetComponent<WindTool> ();
		toolTarget = transform.Find ("ToolTarget");
		//worldSphere = transform.Find ("WorldSphere").GetComponent<Collider> ();
		groundPlane = transform.Find("GroundPlane").transform;
	}


	private void Start() 
	{
		isOn = false;
	}


	private void Update() 
	{
		// update sphere scale
		//worldSphere.transform.localScale = new Vector3 (sphereScale, sphereScale, sphereScale);

		if (isOn) {
			// detect collision of water stream with a fire
			// if colliding, balance hot/cold balance
		} 
	}


	public void SetToolTransform(Vector3 globalPosition, Quaternion globalRotation) 
	{
		toolTarget.position = globalPosition;
		toolTarget.rotation = globalRotation;
	}


	public Transform GetToolTransform()
	{
		return toolTarget.transform;
	}


	public void SwitchOn() 
	{
		isOn = true;

		windTool.SwitchOn ();
	}


	public void SwitchOff() 
	{
		isOn = false;

		windTool.SwitchOff ();
	}
}
