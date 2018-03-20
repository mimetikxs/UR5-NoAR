using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Hide/shows the popup


public class TrackingPopup : MonoBehaviour 
{


	void Start() 
	{
	}


	void Update() 
	{
	}


	public void Show() 
	{
		this.gameObject.SetActive (true);
	}


	public void Hide()
	{
		this.gameObject.SetActive (false);
	}
}
