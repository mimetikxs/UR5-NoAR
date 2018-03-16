using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour 
{
	public Vector3 gravity = new Vector3();
	public Ball ball;


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


	public void OnActionDown()
	{
		//magnetTool.SwitchOn ();

		ball.Reset ();
	}


	public void OnActionUp()
	{
		//magnetTool.SwitchOff ();
	}


	public void OnHotspotClicked(Transform hotspotTransform)
	{
//		Vector3 p = hotspotTransform.position;
//		Quaternion r = hotspotTransform.rotation;
//
//		robot.setTargetTransform (p, r);
	}
}
