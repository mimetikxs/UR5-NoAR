using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceObject : MonoBehaviour 
{
	public float radius = 32f;
	public float speed = 2f;
	public bool reverseRotation = false;
	public float rotOffset = 0f;
	public bool isGood = false;

	private Transform _wrapper;
	private GameObject _object;
//	private float _rotDirection = 1f;
//	private bool _reverseRotation = false;

	private float prevRotOffset;


	private void Awake()
	{
		_wrapper = this.transform.Find ("Wrapper");
		_object = this.transform.Find ("Wrapper/Object").gameObject;

		prevRotOffset = rotOffset;
	}


	private void Start() 
	{
	}
	

	private void Update() 
	{
	}


	private void FixedUpdate()
	{

		if (rotOffset != prevRotOffset) 
		{
			_wrapper.transform.Rotate (0f, rotOffset, 0f);
			prevRotOffset = rotOffset;
		}

		float rotDirection = reverseRotation ? -1f : 1f;
		_wrapper.transform.Rotate (0f, speed * rotDirection, 0f);
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
