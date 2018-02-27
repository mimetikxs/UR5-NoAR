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

	private Transform tool;
	private Transform toolTarget;
	private Collider worldSphere;
	private Transform groundPlane;
	private Vector3 force;

	private ParticleSystem particleSystem;

	private bool isOn;



	private void Start() 
	{
		tool = transform.Find ("Tool");
		toolTarget = transform.Find ("ToolTarget");
		worldSphere = transform.Find ("WorldSphere").GetComponent<Collider> ();
		groundPlane = transform.Find("GroundPlane").transform;
		particleSystem = transform.Find("Tool/ParticleSystem").GetComponent<ParticleSystem> ();

		isOn = false;
	}


	private void Update() 
	{
		// update sphere scale
		//worldSphere.transform.localScale = new Vector3 (sphereScale, sphereScale, sphereScale);

		if (isOn) {
			// update force
			Vector3 emitterFloor = toolTarget.position;
			emitterFloor.y = groundPlane.position.y;

			force = Vector3.Normalize (groundPlane.position - emitterFloor) * strength;
		} else {
			force = Vector3.zero;
		}
	}


	private void OnEnable() 
	{
		
	}


	private void OnDisable() 
	{
		
	}


	private void OnDestroy()
	{
		
	}


	private IEnumerator ParticleBurst()
	{
		while (true)
		{
			particleSystem.Emit (20);
			yield return new WaitForSeconds(0.3f);
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

		StartCoroutine("ParticleBurst");
	}


	public void SwitchOff() 
	{
		isOn = false;

		StopCoroutine("ParticleBurst");
	}
}
