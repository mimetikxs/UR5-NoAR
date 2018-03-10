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

	// delegated (triggered when waste is collected)
	public delegate void WasteCollectAction();			
	public event WasteCollectAction OnWasteCollected;


	void Start() 
	{
		isOn = false;
	}
	

	void Update() 
	{
//		this.transform.position = tcp.position;
//		this.transform.rotation = tcp.rotation;
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


	private void OnDisable() 
	{
		SwitchOff ();
	}


	public void SwitchOn()
	{
		isOn = true;
		// TODO enable fx
	}


	public void SwitchOff() 
	{
		isOn = false;
		// TODO disable fx
	}


	public bool IsOn()
	{
		return isOn;
	}
}
