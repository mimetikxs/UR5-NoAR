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

	private Hotspot selectedHotspot = null;

	public Hotspot[] connections;


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
		if (selectedHotspot == null)
			return;

		Vector3 selectedPos = selectedHotspot.transform.GetChild(1).position;
		connections = selectedHotspot.connectedHostspots;

		foreach (Hotspot hs in connections) 
		{
			Debug.DrawLine (selectedPos, hs.transform.GetChild(1).position, Color.green);
		}
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
		selectedHotspot = hotspot;

		// debug TODO: coment out for production
		Transform t = hotspot.transform.Find("Transform").transform;
		fresnelTool.transform.position = t.position;
		fresnelTool.transform.rotation = t.rotation;
	}
}
