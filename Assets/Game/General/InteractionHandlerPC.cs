using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles user input when testing on pc.
 */

public class InteractionHandlerPC : MonoBehaviour 
{
	public Camera camera;

	private LayerMask layerHostpots;


	private void Awake()
	{
		layerHostpots = 1 << LayerMask.NameToLayer ("Hotspots");	
	}


	private void Start () 
	{
	}
	

	private void Update() 
	{
		if (Input.GetKeyDown ("space")) 
		{
			gameObject.SendMessage ("OnActionDown");
		} 
		else if (Input.GetKeyUp ("space")) 
		{
			gameObject.SendMessage ("OnActionUp");
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			IntersectHotspots (Input.mousePosition);
		}		
	}


	private bool IntersectHotspots(Vector3 sreenPosition) 
	{		
		Ray ray = camera.ScreenPointToRay(sreenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000f, layerHostpots))
		{
			gameObject.SendMessage ("OnHotspotClicked", hit.transform);

			return true;
		}

		return false;
	}
}
