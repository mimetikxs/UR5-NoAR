﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Handles logic to extinguish fires.
 * Controls the state of the virtual tool.
 * 
 * TODO: control position of hotspots
 */

public class RigLight : MonoBehaviour 
{
	//private Collider worldSphere;
	private Transform toolTarget;
	private FresnelTool fresnelTool;

	private bool isOn;


	private void Awake()
	{
		fresnelTool = transform.Find ("Tool").GetComponent<FresnelTool> ();
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

		fresnelTool.SwitchOn ();
	}


	public void SwitchOff() 
	{
		isOn = false;

		fresnelTool.SwitchOff ();
	}
}