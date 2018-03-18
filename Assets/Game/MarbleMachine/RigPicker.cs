using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigPicker : MonoBehaviour 
{
	public UR5Controller robot;

	private Transform picker;
	private Vector3 targetPosition;
	private Transform homeTransform;

	private bool targetReached;
	private bool homeReached;

	// delegated:
	public delegate void TargetReachedAction();			
	public event TargetReachedAction OnTargetReached;


	private void Awake()
	{
		picker = transform.Find ("Tool");
		homeTransform = transform.Find ("HotspotHome/transform");
		targetPosition = Vector3.zero;
	}


	private void Start() 
	{
		targetReached = false;
	}


	private void FixedUpdate()
	{
		// check if picker has reached the target position
		Vector3 delta = targetPosition - picker.position;
		float dist = Vector3.Magnitude (delta);
		if (dist < 0.1f  &&  !targetReached) 
		{
			if (OnTargetReached != null)
				OnTargetReached ();
		}
	}
		

	public void DisableHotspots()
	{
		Transform node = transform.Find("Hotspots");
		foreach (Transform item in node) 
		{
			item.gameObject.SetActive (false);;
		}
	}


	public void EnableHotspots()
	{
		Transform node = transform.Find("Hotspots");
		foreach (Transform item in node) 
		{
			item.gameObject.SetActive (true);
		}
	}


	public void SetTargetTransform(Vector3 p, Quaternion r)
	{
		targetPosition = p;
		targetReached = false;

		robot.setTargetTransform (p, r);

		// debugging ///////
		// this is updated every frame by PickerTool 
		picker.position = p;
		picker.rotation = r;
		////////////////////
	}


	public void GoToHome()
	{
		robot.setTargetTransform (homeTransform.position, homeTransform.rotation);

		// debugging ///////
		// this is updated every frame by PickerTool 
		picker.position = homeTransform.position;
		picker.rotation = homeTransform.rotation;
		////////////////////
	}


	public Vector3 GetPickerPosition()
	{
		return picker.position;
	}


	public bool IsHome() 
	{
		return homeReached;
	}
}
