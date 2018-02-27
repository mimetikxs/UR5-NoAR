using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// Control TCP selecting hotspots in the rig

public class ControllerBoatPc : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public Camera camera;

	private RigBoat rig;
	private PlayerBoatBehaviour player;

	private LayerMask layerHostpots;


	void Awake ()
	{
		rig = gameWorld.Find ("Rig").GetComponent<RigBoat> ();
		player = gameWorld.Find ("Player").GetComponent<PlayerBoatBehaviour> ();

		layerHostpots = 1 << LayerMask.NameToLayer ("Hotspots");	
	}


	void Start () 
	{
	}
	

	void Update () 
	{		
		// user input
		// --------------
		if (Input.GetKeyDown ("space")) 
		{
			rig.SwitchOn ();
		} 
		else if (Input.GetKeyUp ("space")) 
		{
			rig.SwitchOff ();
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


	private void IntersectHotspots (Vector3 sreenPosition) 
	{		
		Ray ray = camera.ScreenPointToRay(sreenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000f, layerHostpots))
		{
			Vector3 p = hit.transform.position;
			Quaternion r = hit.transform.rotation;

			rig.SetToolTransform (p, r);

			robot.setTargetTransform (p, r);
		}
	}
}
