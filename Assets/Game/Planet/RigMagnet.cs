using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Handles logic to extinguish fires.
 * Controls the state of the virtual tool.
 * 
 * TODO: control position of hotspots
 */

public class RigMagnet : MonoBehaviour 
{
	//private Collider worldSphere;
	private Transform toolTarget;
	private MagnetTool magnetTool;

	private bool isOn;


	private void Awake()
	{
		magnetTool = transform.Find ("Tool").GetComponent<MagnetTool> ();
		toolTarget = transform.Find ("ToolTarget");
		//worldSphere = transform.Find ("WorldSphere").GetComponent<Collider> ();
	}


	private void Start() 
	{
		SwitchOff ();
	}


	private void Update() 
	{
		// update sphere scale
		//worldSphere.transform.localScale = new Vector3 (sphereScale, sphereScale, sphereScale);

		if (isOn) {
			
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

		magnetTool.SwitchOn ();

//		TODO: apply attraction force to space object
	}


	public void SwitchOff() 
	{
		isOn = false;

		magnetTool.SwitchOff ();
	}
}
