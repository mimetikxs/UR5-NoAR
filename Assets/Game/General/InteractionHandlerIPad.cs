using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles user input when paying on Ipad.
 */

public class InteractionHandlerIPad : MonoBehaviour 
{
	public Camera camera;
	public Transform gameUI;

	private ButtonHold buttonAction;
	private LayerMask layerHostpots;


	private void Awake()
	{
		buttonAction = gameUI.Find ("BottomBar/ButtonAction").GetComponent<ButtonHold> ();

		layerHostpots = 1 << LayerMask.NameToLayer ("Hotspots");	
	}


	private void Start() 
	{
	}
	

	void Update() 
	{
		if (Input.touchCount > 0  &&  !buttonAction.isPressed) 
		{
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) 
			{
				Vector3 screenPos = new Vector3 (touch.position.x, touch.position.y, 0f);

				bool intersectedHotspot = IntersectHotspots (screenPos);
				if (!intersectedHotspot) 
				{
					OnActionButtonDown ();				
				}
			} 
			else if (touch.phase == TouchPhase.Ended) 
			{
				OnActionButtonUp ();
			}
		}
	}


	private bool IntersectHotspots(Vector3 sreenPosition) 
	{		
		Ray ray = camera.ScreenPointToRay(sreenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000f, layerHostpots))
		{
			gameObject.SendMessage ("OnHotspotClicked", hit.transform);

			return true;
		}

		return false;
	}


	private void OnActionButtonDown()
	{
		gameObject.SendMessage ("OnActionDown");
	}


	private void OnActionButtonUp()
	{
		gameObject.SendMessage ("OnActionUp");
	}


	private void OnEnable()
	{
		buttonAction.OnDown += OnActionButtonDown;
		buttonAction.OnUp += OnActionButtonUp;
	}


	private void OnDisable()
	{
		buttonAction.OnDown -= OnActionButtonDown;
		buttonAction.OnUp -= OnActionButtonUp;
	}
}
