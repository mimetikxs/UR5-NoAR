using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundPlayer : MonoBehaviour 
{
//	public AudioClip goodSound;
//	public AudioClip badSound;
	private AudioSource audio;


	void Awake()
	{
		audio = this.transform.GetComponent<AudioSource> ();

		// find if parent is good guy
//		Transform parent = transform.parent;
//		SpaceObject so = parent.GetComponent<SpaceObject> ();
//		while (so == null) 
//		{
//			parent = parent.parent;
//			so = parent.GetComponent<SpaceObject> ();
//		}
//
//		audio.clip = so.isGoodGuy ? badSound : goodSound;
	}


	void Start() 
	{
	}
	

	void Update() 
	{
	}
		

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Tool") 
		{
			audio.Play ();
		}
	}
}
