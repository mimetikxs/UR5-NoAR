using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpaceObject : MonoBehaviour 
{
	public ParticleSystem wasteExplosion;

	public float radius = 32f;
	public float speed = 2f;
	public float mass = 1f;
	public float rotOffset = 0f;
	public bool isGoodGuy = false;

	public float springStrength = 0.082f;
	public float damping = 0.894f;

	private Vector3 velocity;
	private Vector3 forceAccumulator;

	private Transform _wrapper;
	private Transform _object;
	private Transform _anchor;
	private Transform _orbit;


#if UNITY_EDITOR
	void OnValidate()
	{
		_object = this.transform.Find ("Object");
		_anchor = this.transform.Find ("Wrapper/Anchor");
		_orbit = this.transform.Find ("Wrapper/Orbit");

		// debug: scale orbit
		float s = 10f * (radius / 32f);
		Vector3 scale = _orbit.localScale;
		scale.Set (s, s, s);
		_orbit.localScale = scale;

		// position anchor (TODO: do only on Awake)
		_anchor.localPosition = new Vector3 (0f, 0f, radius);
		_anchor.localRotation = Quaternion.Euler (0f, rotOffset, 0f);

		_object.transform.position = _anchor.position;
	}
#endif


	private void Awake()
	{
		_object = this.transform.Find ("Object");
		_wrapper = this.transform.Find ("Wrapper");
		_anchor = this.transform.Find ("Wrapper/Anchor");
		_orbit = this.transform.Find ("Wrapper/Orbit");

		velocity = Vector3.zero;
		forceAccumulator = Vector3.zero;

		// position anchor (TODO: do only on Awake)
		_anchor.localPosition = new Vector3 (0f, 0f, radius);
		_anchor.localRotation = Quaternion.Euler (0f, rotOffset, 0f);

		_object.transform.position = _anchor.position;
	}


	private void FixedUpdate()
	{
		// rotate anchor
		_wrapper.transform.Rotate (0f, speed, 0f);

		// forces
		CalculateSpringForce ();

		// integrate
		velocity += forceAccumulator;
		velocity *= damping;

		forceAccumulator = Vector3.zero;

		// apply
		_object.transform.position += velocity;
	}


	private void CalculateSpringForce()
	{
		Vector3 delta = _anchor.position - _object.position;
		float distance = Vector3.Magnitude (delta);
		Vector3 force = Vector3.Normalize(delta) * (distance * springStrength);

		forceAccumulator += force;

		Debug.DrawLine (_anchor.position, _object.position, Color.red); 
	}


	public void AttractTo(Attractor attractor)
	{
		Vector3 delta = attractor.transform.position - _object.position;
		float distance = Vector3.Magnitude (delta);

		if (distance > attractor.maxDist)
			return;

		distance = Mathf.Clamp(distance, attractor.minDist, attractor.maxDist);
		float attractionStrength = attractor.strength * 1f * mass / (distance * distance); // assume magnet always has mass = 1
		Vector3 direction = Vector3.Normalize (delta);
		Vector3 force = direction * attractionStrength;

		forceAccumulator += force; 
	}


	public void Remove()
	{
		ParticleSystem explosion = (ParticleSystem)Instantiate (wasteExplosion, _object.transform.position, _object.transform.rotation);

		Destroy (gameObject);
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
