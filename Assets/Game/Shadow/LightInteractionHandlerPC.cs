using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractionHandlerPC : MonoBehaviour 
{
	public Camera camera;

	private LayerMask layerHostpots;

	private bool isDragging = false;


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
			if (IntersectHotspots (Input.mousePosition)) 
			{
				isDragging = true;
			}
		} 
		else if (Input.GetMouseButtonUp (0)) 
		{
			isDragging = false;
		}

		if (isDragging) 
		{
			gameObject.SendMessage ("OnDrag", Input.mousePosition);
		}
	}


	private bool IntersectHotspots(Vector3 sreenPosition) 
	{		
		Ray ray = camera.ScreenPointToRay(sreenPosition);
		if (Physics.Raycast (ray, 1000f, layerHostpots))
			return true;

		return false;
	}
}
