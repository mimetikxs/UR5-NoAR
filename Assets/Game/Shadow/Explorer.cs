using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour 
{
	public float speed = 0.02f;

	private Vector3 target;


	private void Awake()
	{
		//rb = transform.GetComponent<Rigidbody> ();
		target = transform.position;
	}


	private void Start() 
	{
	}
	

	private void Update() 
	{
	}


	void FixedUpdate() 
	{
		Vector3 position = transform.position;
		Vector3 delta = target - position;

		position += delta * speed;

		transform.position = position;
		transform.LookAt (target);
	}


	public void SetTarget(Vector3 point)
	{
		target = point;
	}
}
