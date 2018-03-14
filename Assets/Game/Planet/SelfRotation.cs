using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotation : MonoBehaviour 
{
	public float x = 0f;
	public float y = 0f;
	public float z = 0.1f;


	void Start() 
	{
	}
	

	void Update() 
	{
	}


	void FixedUpdate()
	{
		this.transform.Rotate (x, y, z);
	}
}
