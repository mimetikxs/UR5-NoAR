using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the rendering of the virtual tool.
 * Updates position of the virtual tool.
 * Holds state of the virtual tool.
 */

public class ShowerTool : MonoBehaviour {

	public Transform tcp;

	private Transform shower;
	private ParticleSystem particles;
	private bool isOn;


	private void Awake()
	{
		shower = this.transform.Find ("Shower");
		particles = this.transform.Find ("Particles").GetComponent<ParticleSystem> ();
	}


	void Start() 
	{
		isOn = false;
	}
	

	void Update() 
	{
		this.transform.position = tcp.position;
		this.transform.rotation = tcp.rotation;
	}


	private IEnumerator ParticleBurst()
	{
		while (true)
		{
			particles.Emit (1);
			yield return new WaitForSeconds(1f);
		}
	}


	private void OnDisable() 
	{
		SwitchOff ();
	}


	public void SwitchOn()
	{
		isOn = true;
		StartCoroutine("ParticleBurst");
	}


	public void SwitchOff() 
	{
		isOn = false;
		StopCoroutine("ParticleBurst");
	}
}
