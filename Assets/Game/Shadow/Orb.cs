using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour 
{
	public ParticleSystem wasteExplosion;


	private void Awake()
	{
	}


	public void Remove()
	{
		ParticleSystem explosion = (ParticleSystem)Instantiate (wasteExplosion, transform.position, transform.rotation);

		Destroy (gameObject);
	}
}
