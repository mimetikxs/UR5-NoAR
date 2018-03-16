using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour 
{
	public Vector3 gravity = new Vector3();


	void Awake()
	{
		Physics.gravity = gravity;
	}


	void Start() 
	{
	}


	void Update() 
	{
	}


	void FixedUpdate()
	{
	}
}
