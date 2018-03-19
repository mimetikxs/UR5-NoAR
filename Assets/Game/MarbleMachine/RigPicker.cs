using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigPicker : MonoBehaviour 
{
	public bool isDebugging = true;

	public UR5Controller robot;

	private Transform picker;
	private Transform pickerBallTransform;
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

		pickerBallTransform = picker.transform.Find ("BallPos");

		// debug
		picker.GetComponent<PickerTool> ().isDebugging = isDebugging;
	}


	private void Start() 
	{
		targetReached = false;
	}


	private void FixedUpdate()
	{
		// check if picker has reached the target position
		float dist = Vector3.Magnitude (targetPosition - picker.position);
		if (dist < 0.3f  &&  !targetReached) 
		{
			targetReached = true;

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

		if (isDebugging) 
		{
			picker.position = p;
			picker.rotation = r;
		}
	}


	public void GoToHome()
	{
		robot.setTargetTransform (homeTransform.position, homeTransform.rotation);

		if (isDebugging) 
		{
			picker.position = homeTransform.position;
			picker.rotation = homeTransform.rotation;
		}
	}


	public void ReleaseHand()
	{
		Transform fingers = picker.Find ("Picker");
		foreach (Transform finger in fingers) 
		{
			Animation anim = finger.GetComponent<Animation> ();
			//anim["Take 001"].wrapMode = WrapMode.Once;
			anim.Play ("Take 001");
		}
	}


	public Vector3 GetPickerPosition()
	{
		return pickerBallTransform.position;
	}


	public bool IsHome() 
	{
		return homeReached;
	}
}
