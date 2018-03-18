using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour 
{
	public float speed = 0.02f;

	private Vector3 target;
	private AudioSource collectedSound;

	// delegated:
	public delegate void TargetReachedAction();			
	public event TargetReachedAction OnTargetReached;
	public delegate void OrbCollectedAction();
	public event OrbCollectedAction OnOrbCollected;

	public float distance;
	private bool targetReached = false;


	private void Awake()
	{
		target = transform.position;

		collectedSound = GetComponent<AudioSource>();
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
		distance = Vector3.Magnitude (delta);

		if (distance < 2f) 
		{
			if (!targetReached) 
			{
				targetReached = true;

				if (OnTargetReached != null)
					OnTargetReached ();
			}
		}

		if (distance > 0.1f) 
		{
			position += delta * speed;

			transform.position = position;
			transform.LookAt (target);
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Waste") 
		{
			other.GetComponent<Orb> ().Remove ();

			collectedSound.Play ();
			
			if (OnOrbCollected != null) 
				OnOrbCollected();
		}
	}


	public void SetTarget(Vector3 point)
	{
		target = point;
		targetReached = false;
	}
}
