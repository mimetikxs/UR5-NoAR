﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Handles user input when testing on pc (no Vuforia).
 * Manages the main game logic.
 */

public class ControllerMagnet : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public Transform gameUI;

	// physics parameters
	[Range (0f, 1f)] public float springStrength = 0.082f;
	[Range (0f, 1f)] public float springDamping = 0.894f;
	public float attractorStrength = 0.5f;
	public float attractorMinDist = 2f;
	public float attractorMaxDist = 10f;

	// game parameters
	public int startTime = 60;
	private int itemCountGoal;

	private MagnetTool magnetTool;
	private Transform spaceObjects;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;

	// hack
	public Transform initialTransform;

	private bool isPaused = false;

	private int badGuysCount;


	private void Awake()
	{
		magnetTool = gameWorld.Find ("RigMagnet/Tool").GetComponent<MagnetTool> ();
		spaceObjects = gameWorld.Find ("SpaceObjects");

		// ui references
		scorePopup = gameUI.Find("ScorePopup").gameObject;
		lostTrackingPopup = gameUI.Find ("LostTrackingPopup").gameObject;
		bottomBar = gameUI.Find ("BottomBar").gameObject;
		itemCounter = bottomBar.transform.Find ("ItemCounter").GetComponent<ItemCounter> ();
		countDown = bottomBar.transform.Find ("CountDown").GetComponent<CountDown> ();
	}


	private void Start() 
	{
		initItemCounter ();

		countDown.startCount = startTime;
		countDown.Play ();

		// send robot to initial pose
		//OnHotspotClicked (initialTransform);

		isPaused = false;
	}


	private void initItemCounter()
	{
		itemCounter.count = 0;	

		foreach (Transform item in spaceObjects) 
		{
			itemCountGoal += (item.GetComponent<SpaceObject> ().isGoodGuy) ? 0 : 1;
			badGuysCount = itemCountGoal; 
		}
	}
	

	private void Update() 
	{		
	}


	private void FixedUpdate()
	{
		if (magnetTool.IsOn ()) 
		{		
			Attractor attractor = magnetTool.GetComponent<Attractor> ();
			attractor.strength = attractorStrength;
			attractor.minDist = attractorMinDist;
			attractor.maxDist = attractorMaxDist;

			foreach (Transform spaceObject in spaceObjects) 
			{
				SpaceObject obj = spaceObject.GetComponent<SpaceObject> ();

				obj.springStrength = springStrength;
				obj.damping = springDamping;
				obj.AttractTo (attractor);
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
		magnetTool.OnWasteCollected += UpdateCount;
		countDown.OnCountdownFinished += FinishGame;
	}


	private void RemoveListeners()
	{
		magnetTool.OnWasteCollected -= UpdateCount;
		countDown.OnCountdownFinished -= FinishGame;
	}


	private void UpdateCount(bool isGoodGuy)
	{
		badGuysCount -= isGoodGuy ? 0 : 1;

		itemCounter.count += isGoodGuy ? -1 : 1;

		if (itemCounter.count == itemCountGoal)
			FinishGame ();

		if (badGuysCount <= 0)
			FinishGame ();

		if (spaceObjects.childCount <= 1) 
			FinishGame ();
	}


	private void FinishGame()
	{
		RemoveListeners ();

		gameWorld.gameObject.SetActive (false); // disable game node

		bottomBar.SetActive (false);

		ShowScorePopup ();

		lostTrackingPopup.GetComponent<TrackingPopup> ().Hide ();
	}


	private void ShowScorePopup()
	{
		ScorePopup popup = scorePopup.GetComponent<ScorePopup> ();

		//set the score
		float timeWeight = 0.2f;
		float collectionWeight = 0.8f;
		float timePerformance = Mathf.Min( 1f, (float)countDown.GetCount () / 20f ); //(float)countDown.startCount;
		float collectionPerformance = (float)itemCounter.count / (float)itemCountGoal;
		float score = timePerformance * timeWeight + collectionPerformance * collectionWeight;
		int stars = (int)(score * 5f);

		string title = FeedbackCopies.GetTitle (stars);
		string message = FeedbackCopies.GetFeedback ("MAGNET", stars);

		popup.SetStars (stars);
		popup.SetTitle(title);
		popup.SetMessage(message);
		popup.Show ();
	}


	public void OnActionDown()
	{
		magnetTool.SwitchOn ();
	}

		
	public void OnActionUp()
	{
		magnetTool.SwitchOff ();
	}


	public void OnHotspotClicked(Transform hotspotTransform)
	{
		Vector3 p = hotspotTransform.position;
		Quaternion r = hotspotTransform.rotation;

		robot.setTargetTransform (p, r);
	}


	public void Pause()
	{
		if (!isPaused) {
			isPaused = true;
			countDown.Pause ();
			bottomBar.SetActive (false);	
		}
	}


	public void Resume()
	{
		if (isPaused) 
		{
			isPaused = false;
			bottomBar.SetActive (true);
			countDown.Play ();
		}
	}
}
