using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// Control TCP selecting hotspots in the rig

public class Controller2 : MonoBehaviour 
{
	public Rig2 rig;
	public PlayerBehaviour player;
	public UR5Controller robot;
	public ParticleSystem particleSystem;
	public Camera camera;

//	private Plane groundPlane;

	private LayerMask layerHostpots;


	void Start () 
	{
		layerHostpots = 1 << LayerMask.NameToLayer("Hotspots");
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
			IntersectHotspots (Input.mousePosition);
		}

		// update player
		// -------------
		Vector3 windForce = rig.GetForce();

		player.addForce (windForce);
	}


	private void IntersectHotspots(Vector3 sreenPosition) 
	{		
//		Ray ray = Camera.main.ScreenPointToRay(sreenPosition);
		Ray ray = camera.ScreenPointToRay(sreenPosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 1000f, layerHostpots))
		{
			rig.setHotspot (hit.transform);

			robot.setTarget (hit.transform.position, hit.transform.rotation);
		}
	}
}
