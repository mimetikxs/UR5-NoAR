using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if (UNITY_EDITOR)
[ExecuteInEditMode]
#endif
public class HotspotsDebug : MonoBehaviour 
{
	public bool lightEnabled = false;
	public bool autoLookAt = true;
	public bool drawDirection = true;
//	public bool drawTargets = false;


	void Start() 
	{		
		#if (UNITY_EDITOR)
		gameObject.BroadcastMessage ("EnableLight", lightEnabled);
		gameObject.BroadcastMessage ("EnableLookAt", autoLookAt);
		gameObject.BroadcastMessage ("EnableDrawLine", drawDirection);
//		gameObject.BroadcastMessage ("EnableDrawTargets", drawTargets);
		#endif
	}


	void OnValidate()
	{
		#if (UNITY_EDITOR)
		gameObject.BroadcastMessage ("EnableLight", lightEnabled);
		gameObject.BroadcastMessage ("EnableLookAt", autoLookAt);
		gameObject.BroadcastMessage ("EnableDrawLine", drawDirection);
//		gameObject.BroadcastMessage ("EnableDrawTargets", drawTargets);
		#endif
	}
}
