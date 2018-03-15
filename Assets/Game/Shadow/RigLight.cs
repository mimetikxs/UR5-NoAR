using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the state of the virtual light.
 */

public class RigLight : MonoBehaviour 
{
	public Camera camera;

	public float topPointOffsetY = 10f;

	private Transform hotspot;
	private FresnelTool fresnelTool;

	private Collider worldSphere;
	private Vector3 playerPos;
	private Vector3 playerTopPoint;
	private Plane groundPlane;


	private bool isOn;


	private void Awake()
	{
		fresnelTool = transform.Find ("Tool").GetComponent<FresnelTool> ();
		hotspot = transform.Find ("Hotspots/Hotspot");
		worldSphere = transform.Find ("WorldSphere").GetComponent<Collider> ();

		groundPlane = new Plane (Vector3.up, Vector3.zero);

		SwitchOff ();
	}


	private void Start() 
	{
	}


	private void Update() 
	{
		DrawDebug ();
	}


	private void DrawDebug() {
//		Vector3 spotPosition = light.transform.position;
//
//		Debug.DrawLine (spotPosition, hitTop, Color.white);
//		Debug.DrawLine (spotPosition, hitBottom, Color.green);
//		Debug.DrawLine (spotPosition, hitLeft, Color.green);
//		Debug.DrawLine (spotPosition, hitRight, Color.green);
//		Debug.DrawLine (spotPosition, hitCenter, Color.white);

		Debug.Log (playerTopPoint);

		Debug.DrawLine (playerPos, playerTopPoint, Color.yellow);		
	}


	public Transform GetHotspotTansform()
	{
		return hotspot;
	}


	public void SwitchOn() 
	{
		isOn = true;
		fresnelTool.SwitchOn ();
	}


	public void SwitchOff() 
	{
		isOn = false;
		fresnelTool.SwitchOff ();
	}


	// calculate position of hotspot form the screen cursor
	public void SetHotspotPosition(Vector3 screenCursor, Vector3 playerPosition)
	{
		playerPos = playerPosition;
		playerTopPoint = playerPos + Vector3.up * topPointOffsetY;

		Ray ray = camera.ScreenPointToRay(screenCursor);
		RaycastHit hit;
		if (worldSphere.Raycast (ray, out hit, 1000f))
		{
			// calculate orientation
			// y axys is forward, z is down
			Quaternion rotation = hotspot.transform.rotation;
			Vector3 currDirection = Vector3.Normalize (rotation * Vector3.up);
			Vector3 targetDirection = Vector3.Normalize (playerTopPoint - hit.point);

//			rotation.SetFromToRotation(

			hotspot.position = hit.point;
//			hotspot.rotation.SetFromToRotation (currDirection, targetDirection);

			hotspot.LookAt (playerTopPoint);
		}
	}
}
