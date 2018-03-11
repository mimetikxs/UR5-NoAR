﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaceCamera : MonoBehaviour 
{
	private Camera mainCamera;


	void Start() 
	{
		mainCamera = Camera.main;
	}
	

	void Update()
	{
		transform.LookAt (mainCamera.transform.position);
	}
}
