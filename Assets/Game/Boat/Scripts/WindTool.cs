using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the rendering of the virtual tool.
 * Updates position of the virtual tool.
 * Holds state of the virtual tool.
 */

[RequireComponent(typeof(AudioSource))]
public class WindTool : MonoBehaviour {

	public Transform tcp;

	private Transform fan;
	private ParticleSystem fxRays;
	private ParticleSystem fxSpikes;
	private bool isOn;
	private AudioSource windSound;


	private void Awake()
	{
		fan = this.transform.Find ("Fan");
		fxRays = this.transform.Find ("Fan_Wind/FX_Rays").GetComponent<ParticleSystem> ();
		fxSpikes = this.transform.Find ("Fan_Wind/FX_Spikes").GetComponent<ParticleSystem> ();

		windSound = GetComponent<AudioSource>();
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
			fxRays.Emit (10);
			//fxSpikes.Emit (200);
			yield return new WaitForSeconds(0.4f);
		}
	}


	private void OnDisable() 
	{
		SwitchOff ();

		windSound.Stop ();
	}


	public void SwitchOn()
	{
		isOn = true;
		StartCoroutine("ParticleBurst");
		windSound.Play ();
	}


	public void SwitchOff() 
	{
		isOn = false;
		StopCoroutine("ParticleBurst");
		windSound.Pause ();
	}
}
