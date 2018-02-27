using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls the position of the virtual tool.
 * Controls the state of the virtual tool.
 * Renders the virtual tool.
 * Calculates the force that affects the boat.
 */


public class RigBoat : MonoBehaviour 
{
	public float strength = 0.005f; 	// magnitude of wind force

	// tool pose calculation
	//public float sphereScale = 10f;

	private Transform toolTarget;
	private Collider worldSphere;
	private Transform groundPlane;
	private Vector3 force;

	private bool isOn;


	void Start () 
	{
		toolTarget = transform.Find ("ToolTarget");
		worldSphere = transform.Find ("WorldSphere").GetComponent<Collider> ();
		groundPlane = transform.Find("GroundPlane").transform;

		isOn = false;
	}


	void Update () 
	{
		// update sphere scale
		//worldSphere.transform.localScale = new Vector3 (sphereScale, sphereScale, sphereScale);

		// update force
		if (isOn) {
			// avoids player moving back and forth when reaching target
			Vector3 emitterFloor = emitter.position;
			emitterFloor.y = groundPlane.position.y;

			force = Vector3.Normalize (groundPlane.position - emitterFloor) * strength;
		} else {
			force = Vector3.zero;
		}
	}


	public void SetToolTransform(Transform t) 
	{
		emitter.position = t.position;
		emitter.rotation = t.rotation;
	}


	/*
	 * Returns tool global position
	 */
	public Vector3 GetToolPosition() {
		return emitter.transform.position;
	}


	/*
	 * Returns tool global orientation
	 */
	public Quaternion GetToolOrientation() {
		return emitter.transform.rotation;
	}


	public Vector3 GetForce() 
	{
		return force;
	}


	public void SwitchOn() 
	{
		isOn = true;
	}


	public void SwitchOff() 
	{
		isOn = false;
	}
}
