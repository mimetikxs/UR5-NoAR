using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ControllerBoatIpad : MonoBehaviour 
{
	public RigBoat rig;
	public BoatBehaviour player;
	public UR5Controller robot;
	public ParticleSystem particleSystem;
	public Camera camera;

	private Plane groundPlane;


	void Start () 
	{
		groundPlane = new Plane (Vector3.up, rig.transform.Find("GroundPlane").transform.position);
		Debug.Log (groundPlane);
	}
	

	void Update () 
	{		
		// user input
		// --------------
		if (Input.GetKey ("space")) 
		{
			rig.SwitchOn ();
			particleSystem.Emit (1);
		} 
		else
		{
			rig.SwitchOff ();
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
		Vector3 windForce = rig.GetWindDirection();

		player.addForce (windForce);
	}


	private void IntersectPlane(Vector3 sreenPosition) 
	{		
		
	}
}
