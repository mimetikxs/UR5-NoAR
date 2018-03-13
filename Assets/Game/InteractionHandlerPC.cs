using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles user input when testing on pc.
 */

public class InteractionHandlerPC : MonoBehaviour 
{
	void Start () 
	{
	}
	

	void Update () 
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
			gameObject.SendMessage("OnHotspotClicked", Input.mousePosition);
		}		
	}
}
