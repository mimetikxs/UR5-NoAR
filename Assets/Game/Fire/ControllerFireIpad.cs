﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Handles user input for an iPad device with Vuforia enabled.
 * Manages the main game logic.
 */

public class ControllerFireIpad : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public Transform gameUI;
	public Camera camera;

	// game parameters
	private int itemCountGoal;
	public int startTime = 60;

	private RigShower rig;
	private LayerMask layerHostpots;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;
	private ButtonHold buttonAction;


	private void Awake()
	{
		rig = gameWorld.Find ("RigShower").GetComponent<RigShower> ();

		layerHostpots = 1 << LayerMask.NameToLayer ("Hotspots");	

		// ui references
		scorePopup = gameUI.Find("ScorePopup").gameObject;
		lostTrackingPopup = gameUI.Find ("LostTrackingPopup").gameObject;
		bottomBar = gameUI.Find ("BottomBar").gameObject;
		itemCounter = bottomBar.transform.Find ("ItemCounter").GetComponent<ItemCounter> ();
		countDown = bottomBar.transform.Find ("CountDown").GetComponent<CountDown> ();

		Debug.Log (countDown);

		buttonAction = gameUI.Find ("BottomBar/ButtonAction").GetComponent<ButtonHold> ();
	}


	private void Start() 
	{
		itemCountGoal = 5;//gameWorld.Find ("Waste").childCount;

		countDown.startCount = startTime;
		countDown.Play ();
	}


	private void Update() 
	{	
		// user input
		// --------------
		if (buttonAction.isPressed) 
		{
			rig.SwitchOn ();
		} 
		else 
		{
			rig.SwitchOff ();

			if (Input.touchCount > 0) 
			{
				Touch touch = Input.GetTouch (0);

				if (touch.phase == TouchPhase.Began) {
					Vector3 screenPos = new Vector3 (touch.position.x, touch.position.y, 0f);
					IntersectHotspots (screenPos);
				}
			}
		}
	}


	private void OnEnable()
	{
		AddListeners ();
	}


	private void OnDisable()
	{
		RemoveListeners ();
	}


	private void AddListeners()
	{
		countDown.OnCountdownFinished += FinishGame;
	}


	private void RemoveListeners()
	{
		countDown.OnCountdownFinished -= FinishGame;
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
		itemCounter.count += 1;

		if (itemCounter.count == itemCountGoal)
			FinishGame ();
	}


	private void FinishGame()
	{
		enabled = false; // stop updates

		rig.SwitchOff ();

		RemoveListeners ();

		bottomBar.SetActive (false);

		ShowScorePopup ();
	}


	private void ShowScorePopup()
	{
		ScorePopup popup = scorePopup.GetComponent<ScorePopup> ();

		//set the score
		float timeWeight = 0.2f;
		float collectionWeight = 0.8f;
		float timePerformance = countDown.GetCount() / countDown.startCount;
		float collectionPerformance = itemCounter.count / itemCountGoal;
		float score = timePerformance * timeWeight + collectionPerformance * collectionWeight;

		// TODO: logic to set the text based on score. Read text from an xml
		string title;
		string message;

		popup.SetScore (score);
		//		popup.SetTitle();
		//		popup.SetMessage();
		popup.Show ();
	}
}
