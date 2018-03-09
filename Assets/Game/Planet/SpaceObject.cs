using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceObject : MonoBehaviour 
{
	public float radius = 32f;
	public float speed = 2f;
	public float rotOffset = 0f;
	public bool reverseRotation = false;
	public bool isGood = false;

	private Transform _wrapper;
	private Transform _object;
	private Transform _anchor;
//	private float _rotDirection = 1f;
//	private bool _reverseRotation = false;

	private float prevRotOffset;

	// cheap spring dynamics
	// working in local space
	[Range (0f, 1f)] public float springStrength = 0.082f;
	[Range (0f, 1f)] public float damping = 0.894f;
	private Vector3 attractionForce;
	private Vector3 velocity;
	private Vector3 forceAccumulator;

	public float attractorStrength = 0.5f;
	public float attractorMinDist = 2f;
	public float attractorMaxDist = 10f; // don't apply force from this distance

	// testing...
	public Transform attractor;
	public bool doAttraction = false;

	public bool hasCollided = false;


	private void Awake()
	{
		_wrapper = this.transform.Find ("Wrapper");
		_object = this.transform.Find ("Object");
		_anchor = this.transform.Find ("Wrapper/Anchor");

		prevRotOffset = rotOffset;

		velocity = Vector3.zero;
		attractionForce = Vector3.zero;
		forceAccumulator = Vector3.zero;
	}


	private void Start() 
	{
	}
	

	private void Update() 
	{
	}


	private void FixedUpdate()
	{
		_anchor.localPosition = new Vector3 (0f, 0f, radius);

		if (!hasCollided) {
			RotateAnchor ();

			// forces
			CalculateAttractionForce ();
			CalculateSpringForce ();

			// integrate
			velocity += forceAccumulator;
			velocity *= damping;

			forceAccumulator = Vector3.zero;

			// apply
			_object.transform.position += velocity;
		}
	}


	private void RotateAnchor()
	{
		if (rotOffset != prevRotOffset) 
		{
			_wrapper.transform.Rotate (0f, rotOffset, 0f);
			prevRotOffset = rotOffset;
		}

		float rotDirection = reverseRotation ? -1f : 1f;
		_wrapper.transform.Rotate (0f, speed * rotDirection, 0f);
	}


	private void CalculateSpringForce()
	{
		Vector3 delta = _anchor.position - _object.position;
		float distance = Vector3.Magnitude (delta);
		Vector3 force = Vector3.Normalize(delta) * (distance * springStrength);

		forceAccumulator += force;

		Debug.DrawLine (_anchor.position, _object.position, Color.red); 
	}


	private void CalculateAttractionForce()
	{
		if (!doAttraction)
			return;

		Vector3 attractorPosition = attractor.position;

		Vector3 delta = attractorPosition - _object.position;
		float distance = Vector3.Magnitude (delta);

		if (distance > attractorMaxDist)
			return;

		distance = Mathf.Clamp(distance, attractorMinDist, 100f);
		float attractionStrength = (attractorStrength * 1 * 1) / (distance * distance);
		Vector3 direction = Vector3.Normalize (delta);
		Vector3 force = direction * attractionStrength;

		forceAccumulator += force; 
	}


	public void OnCollisionWithMagnet()
	{
		hasCollided = true;

		// TODO: destroy on collision
	}


//	public bool reverseRotation
//	{
//		get { return _reverseRotation; }
//		set {
//			_reverseRotation = value;
//			_rotDirection = _reverseRotation ? -1f : 1f;
//		}
//	}
}
