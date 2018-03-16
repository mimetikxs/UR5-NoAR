using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour 
{
	public float speed = 0.02f;

	private Vector3 target;

	// delegated:
	// triggered when waste is collected
	public delegate void TargetReachedAction();			
	public event TargetReachedAction OnTargetReached;
	public delegate void OrbCollectedAction();
	public event OrbCollectedAction OnOrbCollected;


	private void Awake()
	{
		target = transform.position;
	}


	private void Start() 
	{
	}
	

	private void Update() 
	{
	}


	private void FixedUpdate() 
	{
		Vector3 position = transform.position;
		Vector3 delta = target - position;

		position += delta * speed;

		transform.position = position;
		transform.LookAt (target);
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Waste") 
		{
			other.GetComponent<WasteBehaviour> ().Remove ();

			//collectedSound.Play ();

			// trigger delegated 
			if (OnOrbCollected != null) 
				OnOrbCollected();
		}
	}


	public void SetTarget(Vector3 point)
	{
		target = point;
	}
}
