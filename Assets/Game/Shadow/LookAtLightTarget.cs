using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class LookAtLightTarget : MonoBehaviour 
{
	private Transform target;


	void Awake()
	{
		target = transform.parent.Find ("LightTarget");	
	}


	void Start () 
	{
		
	}
	

	void Update () 
	{
		//transform.rotation = Quaternion.FromToRotation (transform.position, target.position);
		transform.LookAt(target.position);

		transform.Rotate (90f, 0f, 0f);
	}
}
