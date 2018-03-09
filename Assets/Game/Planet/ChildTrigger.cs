using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		this.transform.parent.GetComponent<SpaceObject> ().OnCollisionWithMagnet ();
	}
}
