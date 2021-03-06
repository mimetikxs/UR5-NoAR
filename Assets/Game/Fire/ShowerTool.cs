﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the rendering of the virtual tool.
 * Updates position of the virtual tool.
 * Holds state of the virtual tool.
 */

public class ShowerTool : MonoBehaviour {

	public float coolingPower = 0.04f; // 0..1 //how much the temp drops per cluster update

	public Transform tcp;

	private ParticleSystem particles;
	private bool isOn;

	private LayerMask layerClusters;


	private AudioSource audio;


	private void Awake()
	{
		particles = this.transform.Find ("Particles").GetComponent<ParticleSystem> ();

		layerClusters = 1 << LayerMask.NameToLayer ("Clusters");

		audio = GetComponent<AudioSource> ();
	}


	void Start() 
	{
		SwitchOff ();
	}
	

	void Update() 
	{
		this.transform.position = tcp.position;
		this.transform.rotation = tcp.rotation;

		if (isOn) 
		{
			particles.Emit (2);
		}
	}


	public TreeCluster GetIntersectedCluster()
	{
		TreeCluster cluster = null;

		if (isOn) 
		{
			Vector3 origin = this.transform.position;
			Vector3 direction = this.transform.rotation * Vector3.up;
			RaycastHit hit;

			if (Physics.Raycast (origin, direction, out hit, 1000f, layerClusters)) 
			{
				cluster = hit.transform.GetComponent<TreeCluster> ();
			}
		}

		return cluster;
	}


	private void OnDisable() 
	{
		SwitchOff ();
	}


	public void SwitchOn()
	{
		isOn = true;
		audio.Play ();
	}


	public void SwitchOff() 
	{
		isOn = false;
		audio.Stop ();
	}
}
