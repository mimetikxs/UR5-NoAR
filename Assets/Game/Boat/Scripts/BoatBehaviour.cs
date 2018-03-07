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
	private Vector3 initialPos;
	private Quaternion initialRot;

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

		waterTrail = this.transform.Find ("Foam_FX").GetComponent<ParticleSystem> ();

		initialPos = this.transform.position;
		initialRot = this.transform.rotation;
	}


	private void FixedUpdate() 
	{
		rb.AddForceAtPosition (force * windScale, forcePoint.position, ForceMode.Acceleration);

		if (isOutsideWorld)
			rb.AddForceAtPosition(Vector3.down * 10f, forcePoint.position, ForceMode.Acceleration);

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
		// detect when the ship leaves the world
		if (other.name == "InWorldArea" && isOutsideWorld == false) 
		{
			isOutsideWorld = true;

//			waterTrail.gameObject.SetActive(false);

			rb.constraints = RigidbodyConstraints.None;

			Invoke("ResetToHome", 2f);
		}
	}


	private void ResetToHome()
	{
		rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		// put ship in initial position
		this.transform.position = initialPos;
		this.transform.rotation = initialRot;

//		waterTrail.gameObject.SetActive(true);

		isOutsideWorld = false;
	}


	// Normalized force (we only need direction)
	public void addForce(Vector3 f) 
	{
		force += f;
	}
}
