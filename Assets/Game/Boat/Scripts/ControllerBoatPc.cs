﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Handles user input when testing on pc (no Vuforia).
 * Manages the main game logic.
 */

public class ControllerBoatPc : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public Transform gameUI;
	public Camera camera;

	// game parameters
	private int itemCountGoal;
	public int startTime = 60;

	private RigBoat rig;
	private BoatBehaviour player;
	private LayerMask layerHostpots;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;


	private void Awake()
	{
		rig = gameWorld.Find ("Rig").GetComponent<RigBoat> ();
		player = gameWorld.Find ("Player").GetComponent<BoatBehaviour> ();

		layerHostpots = 1 << LayerMask.NameToLayer ("Hotspots");	

		// ui references
		scorePopup = gameUI.Find("ScorePopup").gameObject;
		lostTrackingPopup = gameUI.Find ("LostTrackingPopup").gameObject;
		bottomBar = gameUI.Find ("BottomBar").gameObject;
		itemCounter = bottomBar.transform.Find ("ItemCounter").GetComponent<ItemCounter> ();
		countDown = bottomBar.transform.Find ("CountDown").GetComponent<CountDown> ();
	}


	private void Start() 
	{
		itemCountGoal = gameWorld.Find ("Waste").childCount;

		countDown.startCount = startTime;
		countDown.Play ();
	}
	

	private void Update() 
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
		player.OnWasteCollected += IncreaseCount;
		countDown.OnCountdownFinished += FinishGame;
	}


	private void RemoveListeners()
	{
		player.OnWasteCollected -= IncreaseCount;
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
		int stars = (int) (5f * score);
		// TODO: logic to set the text based on score. Read text from an xml
		string title;
		string message;

		popup.SetScore (stars);
//		popup.SetTitle(FeedbackCopies.GetTitle(stars));
		//		popup.SetMessage();
		popup.Show ();
	}
}
