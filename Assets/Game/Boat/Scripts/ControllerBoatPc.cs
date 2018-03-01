using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Handles user input when testing on pc (no Vuforia).
 */

public class ControllerBoatPc : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public ItemCounter counter;
	public Camera camera;

	private RigBoat rig;
	private BoatBehaviour player;
	private LayerMask layerHostpots;

	private 


	void Awake()
	{
		rig = gameWorld.Find ("Rig").GetComponent<RigBoat> ();
		player = gameWorld.Find ("Player").GetComponent<BoatBehaviour> ();

		layerHostpots = 1 << LayerMask.NameToLayer ("Hotspots");	
	}


	void Start() 
	{
	}
	

	void Update() 
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
		Vector3 windDir = rig.GetWindDirection();

		player.addForce (windDir);
	}


	private void IntersectHotspots(Vector3 sreenPosition) 
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


	private void IncreaseCount()
	{
		Debug.Log ("Item collected");
	}


	void OnEnable()
	{
		player.OnWasteCollected += IncreaseCount;
	}


	void OnDisable()
	{
		player.OnWasteCollected -= IncreaseCount;
	}
}
