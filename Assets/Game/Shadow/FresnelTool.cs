using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the rendering of the virtual tool.
 * Updates position of the virtual tool.
 * Holds state of the virtual tool.
 */

public class FresnelTool : MonoBehaviour {

	public Transform tcp;

	private Transform fresnel;
	private Light spotLight;
	private bool isOn;


	private void Awake()
	{
		fresnel = this.transform.Find ("Fresnel");
		spotLight = this.transform.Find ("Spotlight").GetComponent<Light> ();
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


	private void OnDisable() 
	{
		SwitchOff ();
	}


	public void SwitchOn()
	{
		isOn = true;
		spotLight.enabled = true;
	}


	public void SwitchOff() 
	{
		isOn = false;
		spotLight.enabled = false;
	}
}
