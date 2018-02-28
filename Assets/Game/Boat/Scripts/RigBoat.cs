using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the position of the virtual tool.
 * Controls the state of the virtual tool.
 * Renders the virtual tool.
 * Calculates the force that affects the boat.
 * 
 * TODO: control position of hotspots
 */

public class RigBoat : MonoBehaviour 
{
	public float strength = 0.015f; 	// magnitude of wind force
	//public float sphereScale = 10f;

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
			// update force
			Vector3 toolFloor = toolTarget.position;
			toolFloor.y = groundPlane.position.y;

			force = Vector3.Normalize (groundPlane.position - toolFloor) * strength;
		} else {
			force = Vector3.zero;
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


	public Vector3 GetForce() 
	{
		return force;
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
