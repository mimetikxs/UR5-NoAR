using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Handles user input when testing on pc (no Vuforia).
 * Manages the main game logic.
 */

public class ControllerFirePc : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public Transform gameUI;
	public Camera camera;

	// game parameters
	public int startTime = 60;
	private int itemCountGoal;

	private ShowerTool showerTool;
	private Transform clusters;

	private LayerMask layerHostpots;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;


	private void Awake()
	{
		showerTool = gameWorld.Find ("RigShower/Tool").GetComponent<ShowerTool> ();
		clusters = gameWorld.Find ("Clusters");

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
		itemCountGoal = clusters.childCount;
		itemCounter.count = 0;

		countDown.startCount = startTime;
		countDown.Play ();
	}
	

	private void Update() 
	{		
		if (Input.GetKeyDown ("space")) 
		{
			showerTool.SwitchOn ();
		} 
		else if (Input.GetKeyUp ("space")) 
		{
			showerTool.SwitchOff ();
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			IntersectHotspots (Input.mousePosition);
		}
	}


	private IEnumerator UpdateTemperataure()
	{
		while (true)
		{
			// heatup all clusters
			foreach (Transform obj in clusters) 
			{
				TreeCluster cluster = obj.GetComponent<TreeCluster> ();
				cluster.Heatup ();
			}

			// cooldown intersected cluster
			TreeCluster intersectedCluster = showerTool.GetIntersectedCluster();
			if (intersectedCluster != null) 
			{
				intersectedCluster.Cooldown (showerTool.coolingPower);
			}

			yield return new WaitForSeconds(0.3f);
		}
	} 
			

	private void OnEnable()
	{
		AddListeners ();
		StartCoroutine("UpdateTemperataure");
	}


	private void OnDisable()
	{
		RemoveListeners ();
		StopCoroutine ("UpdateTemperataure");
	}


	private void AddListeners()
	{
		//magnetTool.OnWasteCollected += IncreaseCount;
		countDown.OnCountdownFinished += FinishGame;
	}


	private void RemoveListeners()
	{
		//magnetTool.OnWasteCollected -= IncreaseCount;
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
