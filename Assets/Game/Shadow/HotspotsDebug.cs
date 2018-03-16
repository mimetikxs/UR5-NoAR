using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class HotspotsDebug : MonoBehaviour 
{
	public bool lightEnabled = false;
	private bool wasLightEnabled = false;

	public bool autoLookAt = true;
	private bool wasAutoLookAt = true;

	public bool drawDirection = true;
	private bool wasDrawDiretion = true;


	void Start() 
	{		
	}
	

	void Update() 
	{
		// only act when dirty
		if (lightEnabled != wasLightEnabled) 
		{
			gameObject.BroadcastMessage ("EnableLight", lightEnabled);
			wasLightEnabled = lightEnabled;
		}

		if (autoLookAt != wasAutoLookAt) 
		{
			gameObject.BroadcastMessage ("EnableLookAt", autoLookAt);
			wasAutoLookAt = autoLookAt;
		}

		if (drawDirection != wasDrawDiretion) 
		{
			gameObject.BroadcastMessage ("EnableDrawLine", drawDirection);
			wasDrawDiretion = drawDirection;
		}
	}
}
