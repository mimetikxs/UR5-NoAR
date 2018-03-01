using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteBehaviour : MonoBehaviour 
{
	public ParticleSystem wasteExplosion;

	private GameObject model;


	private void Awake()
	{
		model = transform.Find ("Sphere").gameObject;
	}


	public void Remove()
	{
		if (model != null) 
		{
			ParticleSystem explosion = (ParticleSystem)Instantiate (wasteExplosion, transform.position, transform.rotation);

			Destroy (gameObject);
		}
	}
}
