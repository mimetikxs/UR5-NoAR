using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour2 : MonoBehaviour {

	public LightRigController lightRig;

	public float speed = 0.02f;

	private Collider lightCollider;
	private Transform target;

	private Vector3 heading; 	  	// the current heading of the player
	private Vector3 targetPos;		// position of target
	private Vector3 prevTargetPos;
	private Vector3 pos;			// position of this player

	private Collider collidingWall;

	// state flags
	private bool isInsideLight;	
	private bool isColliding;

	private Renderer rend;
	private Color defaultColor;
	private Color currentColor;

	private bool wasOn;


	void Start () {
		isInsideLight = false;
		wasOn = false;

		lightCollider = lightRig.transform.Find ("Collider").GetComponent<Collider> ();
		target = lightRig.transform.Find ("Target").transform;

		rend = GameObject.Find("Body").GetComponent<Renderer> ();
		defaultColor = rend.material.color;
	}


	void Update () {
		
	}


	void FixedUpdate () {
		Debug.DrawLine (transform.position, target.position, Color.yellow); 

		pos = transform.position;
		targetPos = target.position;

		// detect change in position
		bool hasTargetChanged = !targetPos.Equals (prevTargetPos);
		prevTargetPos = targetPos;

		// detect change in on/off
		bool hasLightStateChanged = (lightRig.IsOn () && !wasOn);
		wasOn = lightRig.IsOn ();

		// point heading to target
		if ((hasLightStateChanged || hasTargetChanged) && lightRig.IsOn ()) {
			Vector3 delta = targetPos - pos;
			Vector3 dirToTarget = Vector3.Normalize (delta);
			heading = dirToTarget;
		}

		// check inside light
		float distance = Vector3.Magnitude (targetPos - pos);
		float radius = lightCollider.bounds.extents.x;
		isInsideLight = distance < radius;

		// handle collisions
		if (isColliding && collidingWall != null) {
			Vector3 wallNormal = collidingWall.transform.forward;
			heading = Vector3.Reflect (heading, wallNormal);
			isColliding = false;
		}

		if (isInsideLight && lightRig.IsOn ()) {
			transform.position = pos + heading * speed;
		}

		// look at direction
		transform.rotation = Quaternion.LookRotation (heading);

		updateColor ();
	}


	// collisions bubble up to the closest rigid body 
	// https://stackoverflow.com/questions/41926890/unity-how-to-detect-collision-on-a-child-object-from-the-parent-gameobject
	//
	void OnTriggerEnter(Collider other) {
		if (other.name == "Collider") {
//			isInsideLight = true;
		} else if (other.name == "Cube") {
			isColliding = true;
			collidingWall = other;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.name == "Collider") {
//			isInsideLight = false;
		} else if (other.name == "Cube") {
//			isColliding = false;
			collidingWall = null;
		}
	}


	private void updateColor() {
		if (isColliding) {
			rend.material.color = Color.red;
		} else if (isInsideLight) {
			rend.material.color = Color.green;
		} else {
			rend.material.color = defaultColor;
		}
	}


	// ------------------------
	// Public
	// ------------------------


	public void Kill() {
		// TODO: explosion and Destroy
	}
}
