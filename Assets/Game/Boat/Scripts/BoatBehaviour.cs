using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBehaviour : MonoBehaviour 
{
	public float windScale = 5f;

	private Vector3 force;			// force accumulator
	private Transform forcePoint;	// force applied to this point
	private Rigidbody rb;

	// delegated actions:
	// triggered when waste is collected
	public delegate void WasteCollectAction();			
	public event WasteCollectAction OnWasteCollected;


	private void Awake()
	{
		rb = GetComponent<Rigidbody> ();

		forcePoint = this.transform.Find ("ForcePoint").transform;

		force = Vector3.zero;
	}


	private void FixedUpdate() 
	{
		rb.AddForceAtPosition (force * windScale, forcePoint.position, ForceMode.Acceleration);

		force = Vector3.zero; 
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Waste")
			other.GetComponent<WasteBehaviour>().Remove ();

		// trigger delegated 
		if (OnWasteCollected != null)
			OnWasteCollected();
	}


	// Normalized force (we only need direction)
	public void addForce (Vector3 f) 
	{
		force += f;
	}
}



//public class BoatBehaviour : MonoBehaviour {
//	
//	private Vector3 heading;
//	public Vector3 force;		// force accumulator
//	public Vector3 velocity;	// current velocity
//	private Vector3 position;
//
//
//	void Start () {
//		position = transform.position;
//		velocity = Vector3.zero;
//		force = Vector3.zero;
//	}
//	
//
//	void Update () {
//		// draw debug
//		heading = transform.TransformDirection (Vector3.forward);
//		Debug.DrawLine (transform.position, transform.position + heading, Color.green);
//	}
//
//
//	private void FixedUpdate () {
//		velocity += force;
//		velocity *= 0.9f; // drag
//		position = transform.position + velocity;
//
//		// update pos
//		transform.position = position;
//		// update orientation
//		//if (velocity.magnitude > 0)
//			transform.rotation = Quaternion.LookRotation(velocity);
//
//		force = Vector3.zero; 
//	}
//
//
//	public void addForce (Vector3 f) {
//		force += f;
//	}
//}
