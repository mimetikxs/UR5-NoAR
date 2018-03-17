using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour 
{
	public ParticleSystem fxPrefab;

	private float strength = 2000f;
	private Collider collider;


	void Awake()
	{
		collider = transform.GetComponent<Collider> ();
	}


	void Start() 
	{
	}


	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
		Rigidbody other = collision.rigidbody;

		other.AddForce(contact.normal * (-strength));

		//Instantiate (fxPrefab, transform.position, Quaternion.identity);
		Instantiate(fxPrefab, this.transform);
	}


	public void SetStrength(float val)
	{
		strength = val;
	}
}
