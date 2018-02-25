using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rig2 : MonoBehaviour 
{
	public float strength = 0.005f; 	// magnitude of wind force

	// tool pose calculation
	//public float sphereScale = 10f;

	private Transform emitter;
	private Collider worldSphere;
	private Transform groundPlane;
	private Vector3 force;

	private bool isOn;


	void Start () 
	{
		emitter = transform.Find ("Tool");
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


	public void setHotspot(Transform spot) 
	{
		emitter.position = spot.position;
		emitter.rotation = spot.rotation;
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
