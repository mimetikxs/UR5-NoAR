using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class HotspotsDebug : MonoBehaviour 
{
	public bool lightEnabled = false;
	public bool autoLookAt = true;
	public bool drawDirection = true;


	void Start() 
	{		
	}


	void OnValidate()
	{
		gameObject.BroadcastMessage ("EnableLight", lightEnabled);
		gameObject.BroadcastMessage ("EnableLookAt", autoLookAt);
		gameObject.BroadcastMessage ("EnableDrawLine", drawDirection);
	}
}
