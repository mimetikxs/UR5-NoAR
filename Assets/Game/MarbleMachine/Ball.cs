using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public Attractor attractor;

	private string targetBin;
	private float timeInBin;
	private bool isFinished;

	private Rigidbody rb;

	// factory
	private string[] binNames = {"BlueBin", "YellowBin", "GreenBin"};

	// delegated:
	public delegate void GoodBinAction();			
	public event GoodBinAction OnGoodBin;
	public delegate void BadBinAction();			
	public event BadBinAction OnBadBin;

	// sounds
	public AudioClip[] pinballSounds;
	private AudioSource audio;

	// testing
	Vector3 initialPos;


	void Awake()
	{
		audio = GetComponent<AudioSource> ();

		rb = transform.GetComponent<Rigidbody> ();

		initialPos = transform.position;

		Reset ();
	}


	void Start() 
	{
	}


	void Update() 
	{
	}


	void FixedUpdate()
	{
		Attract ();
	}


	private void Attract()
	{
		Vector3 delta = attractor.transform.position - this.transform.position;
		float distance = Vector3.Magnitude (delta);

		if (distance > attractor.maxDist)
			return;

		distance = Mathf.Clamp(distance, attractor.minDist, attractor.maxDist);
		float attractStrength = attractor.strength / (distance * distance);
		Vector3 direction = Vector3.Normalize (delta);
		Vector3 attractForce = direction * attractStrength;

		rb.AddForce (attractForce);
	}
		

	private void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "RecycleBin") 
		{
			timeInBin += Time.deltaTime;

			if (timeInBin > 3f  &&  !isFinished) 
			{
				isFinished = true;

				EvaluateBin (collider.name);
			}
		}
	}


	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Bouncer") 
		{
			int ran = Random.Range (0, 3);
			audio.clip = pinballSounds [ran];
			audio.Play ();
		}
	}


	private void EvaluateBin(string binName)
	{
		rb.constraints = RigidbodyConstraints.FreezeAll;

		if (binName == targetBin) {
			if (OnGoodBin != null)
				OnGoodBin ();				
		} else {
			if (OnBadBin != null)
				OnBadBin ();
		}
	}


	private void SetColor(Color color)
	{
		Color normalizedColor = new Color (color.r / 255f, color.g / 255f, color.b / 255f); 
		Renderer rend = transform.Find ("Ball").GetComponent<Renderer> ();
		rend.material.SetColor("_Color", normalizedColor);
	}


	public void Reset()
	{
		timeInBin = 0f;
		isFinished = false;

		// randomize target bin
		targetBin = binNames[(int)(Random.Range(0, binNames.Length))];

		switch (targetBin) {
		case "BlueBin":
			SetColor (new Color (8f, 134f, 186f));
			break;
		case "YellowBin":
			SetColor (new Color (252f, 255f, 53f));
			break;
		case "GreenBin":
			SetColor (new Color (12f, 143f, 1f));
			break;
		}

		// testing
		Release();
	}


	public void Release()
	{
		transform.position = initialPos;

		rb.velocity = Vector3.zero;
		rb.constraints = RigidbodyConstraints.None;
	}
}
