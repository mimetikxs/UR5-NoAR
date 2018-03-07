using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class BoatBehaviour : MonoBehaviour 
{
	public float windScale = 5f;

	private Vector3 force;			// force accumulator
	private Transform forcePoint;	// force applied to this point
	private Rigidbody rb;
	private AudioSource collectedSound;
	private ParticleSystem waterTrail;
	private bool isOutsideWorld;

	// delegated:
	// triggered when waste is collected
	public delegate void WasteCollectAction();			
	public event WasteCollectAction OnWasteCollected;


	private void Awake()
	{
		rb = GetComponent<Rigidbody> ();

		forcePoint = this.transform.Find ("ForcePoint").transform;

		force = Vector3.zero;

		collectedSound = GetComponent<AudioSource>();

//		waterTrail = 
	}


	private void FixedUpdate() 
	{
		rb.AddForceAtPosition (force * windScale, forcePoint.position, ForceMode.Acceleration);

		force = Vector3.zero; 
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Waste") 
		{
			other.GetComponent<WasteBehaviour> ().Remove ();

			collectedSound.Play ();
		}

		// trigger delegated 
		if (OnWasteCollected != null)
			OnWasteCollected();
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.name == "InWorldArea") 
		{
			isOutsideWorld = true;
		}
	}


	// Normalized force (we only need direction)
	public void addForce (Vector3 f) 
	{
		force += f;
	}
}
