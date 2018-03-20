using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Handles user input when testing on pc (no Vuforia).
 * Manages the main game logic.
 */

public class ControllerLight : MonoBehaviour 
{
	public bool isDebugging = true;

	public Transform gameWorld;
	public UR5Controller robot;
	public Transform gameUI;

	// game parameters
	private int itemCountGoal;
	public int startTime = 60;

	private RigLight rig;
	private Explorer player;
	private LayerMask layerHostpots;
	private Hotspot selectedHotspot;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;

	//public Hotspot inititalHotspot;


	private void Awake()
	{
		rig = gameWorld.Find ("RigLight").GetComponent<RigLight> ();
		player = gameWorld.Find ("Player").GetComponent<Explorer> ();

		layerHostpots = 1 << LayerMask.NameToLayer ("Hotspots");	

		// ui references
		scorePopup = gameUI.Find("ScorePopup").gameObject;
		lostTrackingPopup = gameUI.Find ("LostTrackingPopup").gameObject;
		bottomBar = gameUI.Find ("BottomBar").gameObject;
		itemCounter = bottomBar.transform.Find ("ItemCounter").GetComponent<ItemCounter> ();
		countDown = bottomBar.transform.Find ("CountDown").GetComponent<CountDown> ();


		selectedHotspot = rig.transform.Find ("Hotspots/Hotspot (5)").GetComponent<Hotspot> ();
		//OnHotspotClicked (selectedHotspot.transform);
	}


	private void Start() 
	{
		itemCountGoal = gameWorld.Find ("Orbs").childCount;

		countDown.startCount = startTime;
		countDown.Play ();

		// debugging
		//rig.isDebugging = isDebugging;
	}
	

	private void Update() 
	{		
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
		player.OnTargetReached += ShowActiveHotspot;
		player.OnOrbCollected += IncreaseCount;
		countDown.OnCountdownFinished += FinishGame;
	}


	private void RemoveListeners()
	{
		player.OnTargetReached -= ShowActiveHotspot;
		player.OnOrbCollected -= IncreaseCount;
		countDown.OnCountdownFinished -= FinishGame;
	}


	private void ShowActiveHotspot()
	{
		rig.SetActiveHostpot (selectedHotspot);
	}


	private void IncreaseCount()
	{
		itemCounter.count += 1;

		if (itemCounter.count == itemCountGoal)
			FinishGame ();
	}


	private void FinishGame()
	{
		RemoveListeners ();

		gameWorld.gameObject.SetActive (false); // disable game node

		bottomBar.SetActive (false);

		ShowScorePopup ();
	}


	private void ShowScorePopup()
	{
		ScorePopup popup = scorePopup.GetComponent<ScorePopup> ();

		//set the score
		float timeWeight = 0.2f;
		float collectionWeight = 0.8f;
		float timePerformance = (float)countDown.GetCount() / (float)countDown.startCount;
		float collectionPerformance = (float)itemCounter.count / (float)itemCountGoal;
		float score = timePerformance * timeWeight + collectionPerformance * collectionWeight;
		int stars = (int)(score * 5f);

		string title = FeedbackCopies.GetTitle (stars);
		string message = FeedbackCopies.GetFeedback ("LIGHT", stars);

		popup.SetStars (stars);
		popup.SetTitle(title);
		popup.SetMessage(message);
		popup.Show ();
	}


	public void OnActionDown()
	{
		// noop
	}


	public void OnActionUp()
	{
		// noop
	}


	public void OnHotspotClicked(Transform hotspotTransform)
	{
		Vector3 p = hotspotTransform.position;
		Quaternion r = hotspotTransform.rotation;
		Hotspot hotspot = hotspotTransform.parent.GetComponent<Hotspot> ();


		Debug.Log (hotspotTransform.parent);

		player.SetTarget (hotspot.GetGroundPosition ());

		rig.DisableHotspots ();

		selectedHotspot = hotspot;

		robot.setTargetTransform (p, r);
	}
}
