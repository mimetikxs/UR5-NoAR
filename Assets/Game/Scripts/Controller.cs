using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Controller : MonoBehaviour 
{
	public Rig windRig;
	public PlayerBehaviour player;
	public Camera camera;

	public UR5Controller robot;

	private Plane groundPlane;


	void Start () 
	{
		groundPlane = new Plane (Vector3.up, windRig.transform.parent.Find("Ground").transform.position);
		Debug.Log (groundPlane);
	}
	

	void Update () 
	{		
		// user input
		// --------------
		if (Input.GetKey ("space")) 
		{
			windRig.SwitchOn ();
		} else {
			windRig.SwitchOff ();
		}

		if (Input.GetKey ("h")) 
		{
			robot.goHome ();
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			IntersectPlane (Input.mousePosition);
		}

		// update player
		// -------------
		Vector3 windForce = windRig.GetForce();

		player.addForce (windForce);
	}


	private void IntersectPlane(Vector3 sreenPosition) 
	{		
		Ray ray = camera.ScreenPointToRay(sreenPosition);

		float enter = 0.0f;
		if (groundPlane.Raycast (ray, out enter)) 
		{			
			Vector3 hitPos = ray.GetPoint(enter);	//Get the point that is clicked

			windRig.setTarget (hitPos);

			robot.setTarget (windRig.GetToolPosition(), windRig.GetToolOrientation ());
		}
	}
}
