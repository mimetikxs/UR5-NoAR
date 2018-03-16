using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour 
{	
	public float speedInner = 0f;
	public float speedMiddle = 0f;
	public float speedOuter = 0f;

	private Transform inner;
	private Transform middle;
	private Transform outer;


	void Awake()
	{
		inner = transform.Find ("Rings/Inner");
		middle = transform.Find ("Rings/Middle");
		outer = transform.Find ("Rings/Outer");
	}


	void Start() 
	{
	}
	

	void Update() 
	{
	}


	void FixedUpdate()
	{
		inner.Rotate (0f, 0f, speedInner);
		middle.Rotate (0f, 0f, speedMiddle);
		outer.Rotate (0f, 0f, speedOuter);
	}
}
