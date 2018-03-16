using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour 
{
	private float strength = 2000f;
	private Collider collider;


	void Awake()
	{
		collider = transform.GetComponent<Collider> ();
	}


	void Start() 
	{
		
	}
	

	void Update() 
	{
		
	}


	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
		Rigidbody other = collision.rigidbody;

		other.AddForce(contact.normal * (-strength));

	}


	public void SetStrength(float val)
	{
		strength = val;
	}
}
