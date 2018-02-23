using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rig : MonoBehaviour 
{
	public PlayerBehaviour player;

	public float strength = 0.005f; 	// magnitude of wind force

	// tool pose calculation
	public float minHeight = 2f;
	public float sphereScale = 10f;

	private Transform emitter;
	private Transform target;
	private Collider worldSphere;
	private Vector3 topPoint;		// top point of the player
	private Vector3 force;
	private Transform groundPlane;

	private bool isOn;


	void Start () 
	{
		emitter = transform.Find ("Tool");
		target = transform.Find ("Target");
		worldSphere = transform.Find ("WorldSphere").GetComponent<Collider> ();
		groundPlane = transform.Find("GroundPlane").transform;

		isOn = false;
	}


	void Update () 
	{
		// update top point
		Vector3 playerPos = player.transform.position;
		topPoint = playerPos + Vector3.up * minHeight;

		// update sphere scale
		worldSphere.transform.localScale = new Vector3 (sphereScale, sphereScale, sphereScale);

		// update force
		if (isOn) {
			// player moves back and forth when reaching target
			//force = Vector3.Normalize (target.position - playerPos) * strength;

			// avoids player moving back and forth when reaching target
			Vector3 emitterFloor = emitter.position;
			emitterFloor.y = groundPlane.position.y;

			force = Vector3.Normalize (target.position - emitterFloor) * strength;
		} else {
			force = Vector3.zero;
		}

		// debug draw
		transform.Find("SphereTop (debug)").transform.position = topPoint;
		Debug.DrawLine (playerPos, topPoint, Color.yellow);					// min height point
		Debug.DrawLine (emitter.position, target.position, Color.red);		// TCP to target
		Debug.DrawLine (playerPos, playerPos + (force * 20f), Color.red);   // force vector
	}


//	void OnDrawGizmos() 
//	{
//		Gizmos.color = Color.green;
//		Gizmos.DrawWireSphere (worldSphere.transform.position, worldSphere.bounds.extents.x);
//	}


	public void setTarget(Vector3 targetPoint) 
	{
		// check if inside sphere
		float maxDist = worldSphere.bounds.extents.x;
		float dist = Vector3.Magnitude (targetPoint);
		if (dist > maxDist) {
			return;
		}

		target.position = targetPoint;

		// calculate emitter position and orientation
		Vector3 dir = Vector3.Normalize (topPoint - targetPoint);
		Ray ray = new Ray (targetPoint, dir);
		// Raycast does not work if casting from inside sphere. Reverse the ray as a workarround
		//https://answers.unity.com/questions/129715/collision-detection-if-raycast-source-is-inside-a.html
		ray.origin = ray.GetPoint(1000f);
		ray.direction = -ray.direction;
		//
		RaycastHit hit;
		if (worldSphere.Raycast (ray, out hit, 1000f)) {
			emitter.position = hit.point;
			emitter.LookAt (target); 	// z axys (forward) points to target
			emitter.Rotate(90f, 0, 0);	// y axys points to target
			emitter.Rotate(0, -90f, 0);	// x axys points up
		}
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
