using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour 
{
	// game parameters
	public int startTime = 60;
	private int itemCountGoal;
	public Vector3 gravity = new Vector3();

	// game objects
	public Ball ball;
//	public PickerTool tool;
	public UR5Controller robot;
	public Transform gameUI;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;

	// testing
	Vector3 ballInitialPos;


	private void Awake()
	{
		Physics.gravity = gravity;

		// testing
		ballInitialPos = ball.transform.position;

		// ui references
		scorePopup = gameUI.Find("ScorePopup").gameObject;
		lostTrackingPopup = gameUI.Find ("LostTrackingPopup").gameObject;
		bottomBar = gameUI.Find ("BottomBar").gameObject;
		itemCounter = bottomBar.transform.Find ("ItemCounter").GetComponent<ItemCounter> ();
		countDown = bottomBar.transform.Find ("CountDown").GetComponent<CountDown> ();
	}


	private void Start() 
	{
	}


	private void Update() 
	{
		// check if ball felt outside the machine
		if (ball.transform.position.y < 0f) {
			OnActionDown ();
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
		ball.OnGoodBin += OnGoodBin;
		ball.OnBadBin += OnBadBin;
	}


	private void RemoveListeners()
	{
		ball.OnGoodBin -= IncreaseCount;
		ball.OnBadBin -= OnBadBin;
	}


	private void OnGoodBin()
	{
		itemCounter.count += 1;

		if (itemCounter.count == itemCountGoal)
			FinishGame ();


	}


	private void OnBadBin()
	{

	}


	private void ResetBall()
	{
		ball.transform.position = ballInitialPos;
		ball.Reset ();
		ball.Release ();
	}


	private void FinishGame()
	{
		// TODO
	}


	public void OnActionDown()
	{
		//magnetTool.SwitchOn ();
	}


	public void OnActionUp()
	{
		//magnetTool.SwitchOff ();
	}


	public void OnHotspotClicked(Transform hotspotTransform)
	{
//		Vector3 p = hotspotTransform.position;
//		Quaternion r = hotspotTransform.rotation;
//
//		robot.setTargetTransform (p, r);
	}
}
