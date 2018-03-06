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
	private Vector3 force;
	private ShowerTool showerTool;

	private bool isOn;


	private void Awake()
	{
		showerTool = transform.Find ("Tool").GetComponent<ShowerTool> ();
		toolTarget = transform.Find ("ToolTarget");
		//worldSphere = transform.Find ("WorldSphere").GetComponent<Collider> ();
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
			// detect collision of water stream with a fire cluster
			// if colliding, begin substracting damage from tree
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

		showerTool.SwitchOn ();
	}


	public void SwitchOff() 
	{
		isOn = false;

		showerTool.SwitchOff ();
	}
}
