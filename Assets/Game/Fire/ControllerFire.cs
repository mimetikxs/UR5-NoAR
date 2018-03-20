using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * Manages the main game logic.
 */

public class ControllerFire : MonoBehaviour 
{
	public Transform gameWorld;
	public UR5Controller robot;
	public Transform gameUI;

	// game parameters
	public int startTime = 60;
	private int itemCountTotal;

	private ShowerTool showerTool;
	private Transform clusters;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;

	private int fireCount = 0;

	private AudioSource audio;


	private void Awake()
	{
		showerTool = gameWorld.Find ("RigShower/Tool").GetComponent<ShowerTool> ();
		clusters = gameWorld.Find ("Clusters");

		// ui references
		scorePopup = gameUI.Find("ScorePopup").gameObject;
		lostTrackingPopup = gameUI.Find ("LostTrackingPopup").gameObject;
		bottomBar = gameUI.Find ("BottomBar").gameObject;
		itemCounter = bottomBar.transform.Find ("ItemCounter").GetComponent<ItemCounter> ();
		countDown = bottomBar.transform.Find ("CountDown").GetComponent<CountDown> ();

		audio = GetComponent<AudioSource> ();
	}


	private void Start() 
	{
		itemCountTotal = clusters.childCount;
		itemCounter.count = itemCountTotal;

		countDown.startCount = startTime;
		countDown.Play ();
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
		countDown.OnCountdownFinished += FinishGame;

		foreach (Transform item in clusters) 
		{
			TreeCluster cluster = item.GetComponent<TreeCluster> ();
			cluster.OnBurnt += DecreaseCount;
			cluster.OnBurningStart += OnFireStarted;
			cluster.OnBurningStop += OnFireStoped;
		}
	}


	private void RemoveListeners()
	{
		countDown.OnCountdownFinished -= FinishGame;

		foreach (Transform item in clusters) 
		{
			TreeCluster cluster = item.GetComponent<TreeCluster> ();
			cluster.OnBurnt -= DecreaseCount;
			cluster.OnBurningStart -= OnFireStarted;
			cluster.OnBurningStop -= OnFireStoped;
		}
	}


	private void OnFireStarted()
	{
		fireCount++;
		PlayFireSound ();
	}


	private void OnFireStoped()
	{
		fireCount--;
		PlayFireSound ();
	}


	private void PlayFireSound()
	{
		if (fireCount > 0)
			audio.Play ();
		else
			audio.Stop ();
	}


	private void DecreaseCount()
	{
		itemCounter.count -= 1;

		if (itemCounter.count < 1)
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
		float score = (float)itemCounter.count / 6f; //(float)itemCountTotal;;
		int stars = (int)(score * 5f);

		string title = FeedbackCopies.GetTitle (stars);
		string message = FeedbackCopies.GetFeedback ("SHOWER", stars);

		popup.SetStars (stars);
		popup.SetTitle(title);
		popup.SetMessage(message);
		popup.Show ();
	}


	public void OnActionDown()
	{
		showerTool.SwitchOn ();
	}


	public void OnActionUp()
	{
		showerTool.SwitchOff ();
	}


	public void OnHotspotClicked(Transform hotspotTransform)
	{
		Vector3 p = hotspotTransform.position;
		Quaternion r = hotspotTransform.rotation;

		robot.setTargetTransform (p, r);
	}
}
