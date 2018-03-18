using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour 
{
	// game parameters
	public int startTime = 60;
	private int itemCountGoal = 5;
	public Vector3 gravity = new Vector3();

	// game objects
	public Bins bins;
	public Ball ball;
	public RigPicker rig;
	public Transform gameUI;

	// sound
	public AudioClip soundGood;
	public AudioClip soundBad;
	private AudioSource audio;

	// state
	private enum State { Idle, Transporting };
	private State state;

	// UI
	private GameObject scorePopup;
	private GameObject lostTrackingPopup;
	private GameObject bottomBar;
	private ItemCounter itemCounter;
	private CountDown countDown;


	private void Awake()
	{
		Physics.gravity = gravity;

		audio = GetComponent<AudioSource> ();

		// ui references
		scorePopup = gameUI.Find("ScorePopup").gameObject;
		lostTrackingPopup = gameUI.Find ("LostTrackingPopup").gameObject;
		bottomBar = gameUI.Find ("BottomBar").gameObject;
		itemCounter = bottomBar.transform.Find ("ItemCounter").GetComponent<ItemCounter> ();
		countDown = bottomBar.transform.Find ("CountDown").GetComponent<CountDown> ();
	}


	private void Start() 
	{
		itemCounter.count = 0;

		countDown.startCount = startTime;
		countDown.Play ();

		// game specific
		rig.GoToHome ();
		ResetBall ();
	}


	private void Update() 
	{
	}


	private void FixedUpdate()
	{
		if (state == State.Transporting) 
		{
			ball.transform.position = rig.GetPickerPosition ();
		}
		else 
		{
			// check if ball felt outside the machine
			if (ball.transform.position.y < 0f) 
			{
				ResetBall ();
				PlayPointSound (false);
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
		ball.OnGoodBin += OnGoodBin;
		ball.OnBadBin += OnBadBin;

		rig.OnTargetReached += ReleaseBall;

		countDown.OnCountdownFinished += FinishGame;
	}


	private void RemoveListeners()
	{
		ball.OnGoodBin -= OnGoodBin;
		ball.OnBadBin -= OnBadBin;

		rig.OnTargetReached -= ReleaseBall;

		countDown.OnCountdownFinished -= FinishGame;
	}


	private void IncreaseCounter()
	{
		itemCounter.count += 1;

		if (itemCounter.count == itemCountGoal)
			FinishGame ();
	}


	private void OnGoodBin()
	{
		IncreaseCounter ();
		PlayBinFx (true);
		PlayPointSound (true);
		Invoke("ResetBall", 1.5f);
	}


	private void OnBadBin()
	{
		PlayBinFx (false);
		PlayPointSound (false);
		Invoke("ResetBall", 1.5f);
	}


	private void PlayBinFx(bool isGood)
	{
		bins.Blink (isGood, 5);
	}


	private void PlayPointSound(bool isGood)
	{
		audio.clip = isGood ? soundGood : soundBad;
		audio.Play ();
	}


	private void ResetBall()
	{
		// put the ball into the picker
		state = State.Transporting;
		ball.Reset ();
		rig.EnableHotspots();
	}


	private void ReleaseBall()
	{
		state = State.Idle;
		ball.Release();
		rig.DisableHotspots ();
		Invoke("SendRigHome", 1.5f);
	}


	private void SendRigHome()
	{
		rig.GoToHome ();
	}


	private void FinishGame()
	{
		RemoveListeners ();

		rig.transform.parent.gameObject.SetActive (false); // disable game node

		bottomBar.SetActive (false);

		ShowScorePopup ();
	}


	private void ShowScorePopup()
	{
		ScorePopup popup = scorePopup.GetComponent<ScorePopup> ();

		//set the score
		float score = (float)itemCounter.count / (float)itemCountGoal;
		int stars = (int)(score * 5f);

		string title = FeedbackCopies.GetTitle (stars);
		string message = FeedbackCopies.GetFeedback ("MACHINE", stars);

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
		rig.SetTargetTransform (p, r);
	}
}
