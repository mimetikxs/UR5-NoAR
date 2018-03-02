using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Hide/shows the popup


public class ScorePopup : MonoBehaviour 
{
	public void SetTitle(string title)
	{
	}


	public void SetScore(float score)
	{
		// TODO: render stars based on score
	}


	public void SetMessage(string message)
	{
		
	}


	public void Show() 
	{
		this.gameObject.SetActive (true);
	}


//	public void Hide()
//	{
//		this.gameObject.SetActive (false);
//	}
}
