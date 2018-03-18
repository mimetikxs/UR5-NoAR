using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hotspot : MonoBehaviour 
{
	public int id;
	public string connections = "";
	public Hotspot[] connectedHostspots;

	private int[] connectedIds;


	void Awake()
	{
		// parse strings
		string[] array = connections.Split(' ');
		int count = array.Length;

		// collect ids
		connectedIds = new int[count];
		for (int i = 0; i < count; i++) 
		{
			connectedIds [i] = int.Parse (array [i]);
		}
			
		// collect references 
		connectedHostspots = new Hotspot[count];

		int index = 0;
		Transform hotspots = this.transform.parent;
		foreach (Transform item in hotspots) 
		{
			Hotspot hs = item.GetComponent<Hotspot> ();
			foreach (int id in connectedIds) 
			{
				if (id == hs.id) 
				{
					connectedHostspots [index] = hs;
					index++;
					break;
				}
			}

			if (index == count)
				break;
		}
	}


	void Start () 
	{
	}
	

	void Update () 
	{
	}

	public Vector3 GetGroundPosition()
	{
		return transform.GetChild(1).position;
	}


	public void Hide()
	{
		gameObject.SetActive (false);
	}


	public void Show()
	{
		gameObject.SetActive (true);
	}


//	public void EnableDrawTargets(bool val)
//	{
//		transform.Find ("LightTarget").gameObject.SetActive (val);
//	}
}
