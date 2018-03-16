using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the hiding and showing of the hotspots
 */

public class RigLight : MonoBehaviour 
{
	public Camera camera;
	public Hotspot[] connections;

	private FresnelTool fresnelTool;
	private Hotspot selectedHotspot = null;


	private void Awake()
	{
		fresnelTool = transform.Find ("Tool").GetComponent<FresnelTool> ();

		DisableHotspots ();

		SwitchOn ();
	}


	private void Start() 
	{
	}


	private void Update() 
	{
		foreach (Hotspot connenction in connections) 
		{
			Debug.DrawLine (
				selectedHotspot.GetGroundPosition(), 
				connenction.GetGroundPosition(), 
				Color.green);
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
		

	public void DisableHotspots()
	{
		Transform node = transform.Find("Hotspots");
		foreach (Transform item in node) 
		{
			item.GetComponent<Hotspot> ().Hide ();
		}
	}


	public void SetActiveHostpot(Hotspot hotspot)
	{
		selectedHotspot = hotspot;
		connections = selectedHotspot.connectedHostspots;

		foreach (Hotspot connection in connections) 
		{
			connection.Show ();
		}

		// debug TODO: coment out for production
		Transform t = hotspot.transform.Find("Transform").transform;
		fresnelTool.transform.position = t.position;
		fresnelTool.transform.rotation = t.rotation;
	}
}
