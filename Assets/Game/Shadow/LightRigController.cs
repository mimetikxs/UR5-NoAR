using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRigController : MonoBehaviour {

	public float minHeight = 2f;
	public CharacterBehaviour2 player;

	private Transform target; 	  // light target
	private Light light;	  	  // this light
	private Vector3 spotPosition;
	private Collider worldSphere;
	private Vector3 topPoint;	// top point of the ship

	private Vector3 top;	  	  // spot light directions
	private Vector3 bottom;
	private Vector3 left;
	private Vector3 right;
	private Vector3 center;
	private Vector3 hitTop;		  // intersections with ground plane
	private Vector3 hitBottom;
	private Vector3 hitLeft;
	private Vector3 hitRight;
	private Vector3 hitCenter;

	private Plane groundPlane;
	private Transform collider;
	private float colliderRadius = 0.6f;


	void Start () {
		light = transform.Find ("Spotlight").GetComponent<Light>();
		target = transform.Find ("Target");
		collider = transform.Find ("Collider");
		worldSphere = transform.Find ("SphereWorld").GetComponent<Collider> ();
			
		groundPlane = new Plane (Vector3.up, Vector3.zero);

		SwitchOff ();
	}
	

	void Update () {
		// update top point
		Vector3 playerPos = player.transform.position;
		topPoint = playerPos + Vector3.up * minHeight;

		updateIntersections ();
		drawDebug ();
	}


	private void updateIntersections() {
		spotPosition = light.transform.position;
		float spotAngle = light.spotAngle / 2f;

		// local spot rays
		// this rays are not transformed by light's transform
		top = 	 Quaternion.Euler (-spotAngle, 0f, 0f) * Vector3.forward; // Note: unity uses post-multiplication with matrices and quaternions
		bottom = Quaternion.Euler (spotAngle, 0f, 0f) * Vector3.forward;
		left =   Quaternion.Euler (0f, -spotAngle, 0f) * Vector3.forward;
		right =  Quaternion.Euler (0f, spotAngle, 0f) * Vector3.forward;
		center = Vector3.forward;

		// global spot directions
		// rays with light's transform applied
		top    = light.transform.TransformVector(top);
		bottom = light.transform.TransformVector(bottom);
		left   = light.transform.TransformVector(left);
		right  = light.transform.TransformVector(right);
		center = light.transform.TransformVector(center);

		// intersections using plane (faster)
		Ray rTop    = new Ray(spotPosition, top);
		Ray rBottom = new Ray(spotPosition, bottom);
		Ray rLeft   = new Ray(spotPosition, left);
		Ray rRight  = new Ray(spotPosition, right);
		Ray rCenter = new Ray(spotPosition, center);
		float enter = 0.0f;

		if (groundPlane.Raycast (rTop, out enter)) {			
			hitTop = rTop.GetPoint(enter);	//Get the point that is clicked
		}
		if (groundPlane.Raycast (rBottom, out enter)) {			
			hitBottom = rBottom.GetPoint (enter);
		}
		if (groundPlane.Raycast (rLeft, out enter)) {			
			hitLeft = rLeft.GetPoint(enter);	
		}
		if (groundPlane.Raycast (rRight, out enter)) {			
			hitRight = rRight.GetPoint(enter);	
		}
		if (groundPlane.Raycast (rCenter, out enter)) {
			hitCenter = rCenter.GetPoint(enter);
		}

		// update collider
		collider.transform.position = hitCenter;
		float scale = Vector3.Distance (hitCenter, hitBottom) / colliderRadius;
		collider.transform.localScale = new Vector3(scale, 1f, scale);
	}


	private void drawDebug() {
		Debug.DrawLine (spotPosition, hitTop, Color.white);
		Debug.DrawLine (spotPosition, hitBottom, Color.green);
		Debug.DrawLine (spotPosition, hitLeft, Color.green);
		Debug.DrawLine (spotPosition, hitRight, Color.green);
		Debug.DrawLine (spotPosition, hitCenter, Color.white);

		player.transform.Find ("Sphere (debug)").position = topPoint;			// min height point
		Debug.DrawLine (player.transform.position, topPoint, Color.yellow);		
	}


	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (hitCenter, transform.Find ("Collider").GetComponent<Collider> ().bounds.extents.x);
		Gizmos.DrawWireSphere (worldSphere.transform.position, worldSphere.bounds.extents.x);
	}

	// -----------------------
	// Public
	// -----------------------


//	public Vector3 getPlaneIntersect() {
//		return hitCenter;
//	}


	public void setTarget (Vector3 targetPoint) {
		// calculate tcp pos
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
		// Raycast does not work if casting from inside sphere, workarround is to reverse the ray
		//https://answers.unity.com/questions/129715/collision-detection-if-raycast-source-is-inside-a.html
		ray.origin = ray.GetPoint(1000f);
		ray.direction = -ray.direction;
		//
		RaycastHit hit;
		if (worldSphere.Raycast (ray, out hit, 1000f)) {
			light.transform.position = hit.point;
			light.transform.LookAt (target);
		}		
	}


	public void SwitchOn () {
		collider.GetComponent<Collider> ().enabled = true;
		light.enabled = true;
	}


	public void SwitchOff (){
		collider.GetComponent<Collider> ().enabled = false;
		light.enabled = false;
	}


	public bool IsOn () {
		return light.enabled;
	}
}
