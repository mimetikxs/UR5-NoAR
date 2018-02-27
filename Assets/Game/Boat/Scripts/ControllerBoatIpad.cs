using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ControllerBoatIpad : MonoBehaviour 
{
	public Rig windRig;
	public PlayerBoatBehaviour player;
	public UR5Controller robot;
	public ParticleSystem particleSystem;
	public Camera camera;

	private Plane groundPlane;


	void Start () 
	{
		groundPlane = new Plane (Vector3.up, windRig.transform.Find("GroundPlane").transform.position);
		Debug.Log (groundPlane);
	}
	

	void Update () 
	{		
		// user input
		// --------------
		if (Input.GetKey ("space")) 
		{
			windRig.SwitchOn ();
			particleSystem.Emit (1);
		} 
		else
		{
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
