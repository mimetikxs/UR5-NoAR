using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerTool : MonoBehaviour 
{
	public bool isDebugging = true;

	public Transform tcp;


	void Start() 
	{
	}
	

	void Update() 
	{
		if (!isDebugging) 
		{
			this.transform.position = tcp.position;
			this.transform.rotation = tcp.rotation;
		}
	}

	public void Open()
	{
	}


	public void Close()
	{
	}
}
