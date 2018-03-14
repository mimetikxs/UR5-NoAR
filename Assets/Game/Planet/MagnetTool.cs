using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the rendering of the virtual tool.
 * Updates position of the virtual tool.
 * Holds state of the virtual tool.
 */

public class MagnetTool : MonoBehaviour {
	
	public Transform tcp;

	private bool isOn;
	private ParticleSystem particles;

	// delegated (triggered when waste is collected)
	public delegate void WasteCollectAction();			
	public event WasteCollectAction OnWasteCollected;


	private void Awake()
	{
		particles = this.transform.Find ("FX magnet").GetComponent<ParticleSystem> ();
	}


	private void Start() 
	{
		isOn = false;
	}
	

	void Update()
	{
		this.transform.position = tcp.position;
		this.transform.rotation = tcp.rotation;
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Waste") 
		{
			other.transform.parent.GetComponent<SpaceObject> ().Remove ();

			if (OnWasteCollected != null) 
				OnWasteCollected ();
		}
	}


	private void OnEnable()
	{
		StartCoroutine("UpdateParticleFx");
	}


	private void OnDisable() 
	{
		StopCoroutine("UpdateParticleFx");
		SwitchOff ();
	}


	private IEnumerator UpdateParticleFx()
	{
		while (true)
		{
			if (isOn) 
			{
				particles.Emit (2);
			}
			yield return new WaitForSeconds(0.3f);
		}
	} 


	public void SwitchOn()
	{
		isOn = true;
	}


	public void SwitchOff() 
	{
		isOn = false;
	}


	public bool IsOn()
	{
		return isOn;
	}
}
