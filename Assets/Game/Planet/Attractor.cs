using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour 
{
	public float strength = 0.5f;
	public float minDist = 2f;
	public float maxDist = 10f; // don't apply force from this distance


	void Start()
	{
	}
}
