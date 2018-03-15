using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotspot : MonoBehaviour 
{
	public int id;
	public string connections = "";

	public int[] connectedIds;


	void Awake()
	{
		// parse strings
		string[] array = connections.Split(' ');
		int count = array.Length;
		connectedIds = new int[count];
		for (int i = 0; i < count; i++) 
		{
			connectedIds [i] = int.Parse (array [i]);
		}
	}


	void Start () 
	{
	}
	

	void Update () 
	{
	}
}
