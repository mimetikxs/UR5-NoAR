using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Handles user input when testing on pc (no Vuforia).
 * Manages the main game logic.
 */

public class ControllerMagnetPc : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public Transform gameUI;
	public Camera camera;

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

	private LayerMask layerHostpots;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;


	private void Awake()
	{
		magnetTool = gameWorld.Find ("RigMagnet/Tool").GetComponent<MagnetTool> ();
		spaceObjects = gameWorld.Find ("SpaceObjects");

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
		itemCountGoal = 5;//gameWorld.Find ("Waste").childCount;

		itemCounter.count = 0;

		countDown.startCount = startTime;
		countDown.Play ();
	}
	

	private void Update() 
	{		
		if (Input.GetKeyDown ("space")) 
		{
			magnetTool.SwitchOn ();
		} 
		else if (Input.GetKeyUp ("space")) 
		{
			magnetTool.SwitchOff ();
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			IntersectHotspots (Input.mousePosition);
		}
	}


	private void FixedUpdate()
	{
		if (magnetTool.IsOn ()) 
		{		
			Attractor attractor = magnetTool.GetComponent<Attractor> ();
			attractor.strength = attractorStrength;
			attractor.minDist = attractorMinDist;
			attractor.maxDist = attractorMaxDist;

			foreach (Transform spaceObject in spaceObjects) {
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
		magnetTool.OnWasteCollected += IncreaseCount;
		countDown.OnCountdownFinished += FinishGame;
	}


	private void RemoveListeners()
	{
		magnetTool.OnWasteCollected -= IncreaseCount;
		countDown.OnCountdownFinished -= FinishGame;
	}


	private void IntersectHotspots(Vector3 sreenPosition) 
	{		
		Ray ray = camera.ScreenPointToRay(sreenPosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 1000f, layerHostpots))
		{
			Debug.Log ("ay!");
				
			Vector3 p = hit.transform.position;
			Quaternion r = hit.transform.rotation;

			//rig.SetToolTransform (p, r);

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
