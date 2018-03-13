using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Handles user input for an iPad device with Vuforia enabled.
 * Manages the main game logic.
 */

public class ControllerBoat : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public Transform gameUI;

	// game parameters
	private int itemCountGoal;
	public int startTime = 60;

	private RigBoat rig;
	private BoatBehaviour player;

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

		itemCounter.count = 0;

		countDown.startCount = startTime;
		countDown.Play ();

		//Resources.UnloadUnusedAssets ();
	}


	private void Update() 
	{
		Vector3 windDir = rig.GetWindDirection();
		player.addForce (windDir);
	}



	public void OnActionDown()
	{
		rig.SwitchOn ();
	}


	public void OnActionUp()
	{
		rig.SwitchOff ();
	}


	public void OnHotspotClicked(Transform hotspotTransform)
	{
		Vector3 p = hotspotTransform.position;
		Quaternion r = hotspotTransform.rotation;

		rig.SetToolTransform (p, r);

		robot.setTargetTransform (p, r);
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


	private void IncreaseCount()
	{
		itemCounter.count += 1;

		if (itemCounter.count == itemCountGoal)
			FinishGame ();
	}


	private void FinishGame()
	{
//		enabled = false; // stop updates
//
//		rig.SwitchOff ();
//
//		RemoveListeners ();
//
//		bottomBar.SetActive (false);
//
//		ShowScorePopup ();
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
