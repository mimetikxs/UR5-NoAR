using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the hiding and showing of the hotspots
 */

public class RigLight : MonoBehaviour 
{
	public Camera camera;

	private FresnelTool fresnelTool;


	private void Awake()
	{
		fresnelTool = transform.Find ("Tool").GetComponent<FresnelTool> ();

		SwitchOn ();
	}


	private void initHostpots()
	{
		
	}


	private void Start() 
	{
	}


	private void Update() 
	{
	}


	public void SwitchOn() 
	{
		fresnelTool.SwitchOn ();
	}


	public void SwitchOff() 
	{
		fresnelTool.SwitchOff ();
	}


	public void setActiveHostpot(Hotspot hotspot)
	{
		Debug.Log (hotspot.connectedIds.Length);

// debug TODO: coment out for production
		Transform t = hotspot.transform.Find("Transform").transform;
		fresnelTool.transform.position = t.position;
		fresnelTool.transform.rotation = t.rotation;
	}
}
