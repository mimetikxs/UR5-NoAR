using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the rendering of the virtual tool.
 * Updates position of the virtual tool.
 * Holds state of the virtual tool.
 */

public class WindTool : MonoBehaviour {

	public Transform tcp;

	private Transform fan;
	private ParticleSystem particleSystem;
	private bool isOn;


	private void Awake()
	{
		fan = this.transform.Find ("Fan");
		particleSystem = this.transform.Find ("ParticleSystem").GetComponent<ParticleSystem> ();
	}


	void Start() 
	{
		isOn = false;
	}
	

	void Update() 
	{
		this.transform.position = tcp.position;
		this.transform.rotation = tcp.rotation;

		if (isOn)
			fan.transform.Rotate (0f, 0f, 20f);
	}


	private IEnumerator ParticleBurst()
	{
		while (true)
		{
			particleSystem.Emit (20);
			yield return new WaitForSeconds(0.4f);
		}
	}


	private void OnDisable() 
	{
		StopCoroutine("ParticleBurst");
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
