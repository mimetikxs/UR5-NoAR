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
	public float strength = 0.005f; 	// magnitude of wind force

	//public float sphereScale = 10f;

	private Transform tool;
	private Transform toolTarget;
	private Collider worldSphere;
	private Transform groundPlane;
	private Vector3 force;

	private ParticleSystem particleSystem;

	private bool isOn;



	void Start () 
	{
		tool = transform.Find ("Tool");
		toolTarget = transform.Find ("ToolTarget");
		worldSphere = transform.Find ("WorldSphere").GetComponent<Collider> ();
		groundPlane = transform.Find("GroundPlane").transform;
		particleSystem = transform.Find("Tool/ParticleSystem").GetComponent<ParticleSystem> ();

		isOn = false;
	}


	void Update () 
	{
		// update sphere scale
		//worldSphere.transform.localScale = new Vector3 (sphereScale, sphereScale, sphereScale);

		if (isOn) {
			// emit particles
			particleSystem.Emit (20);

			// update force
			Vector3 emitterFloor = toolTarget.position;
			emitterFloor.y = groundPlane.position.y;

			force = Vector3.Normalize (groundPlane.position - emitterFloor) * strength;
		} else {
			force = Vector3.zero;
		}
	}


	public void SetToolTransform(Vector3 globalPosition, Quaternion globalRotation) 
	{
		toolTarget.position = globalPosition;
		toolTarget.rotation = globalRotation;
	}


	public Transform GetToolTransform ()
	{
		return toolTarget.transform;
	}


	public Vector3 GetForce () 
	{
		return force;
	}


	public void SwitchOn () 
	{
		isOn = true;
	}


	public void SwitchOff () 
	{
		isOn = false;
	}
}
