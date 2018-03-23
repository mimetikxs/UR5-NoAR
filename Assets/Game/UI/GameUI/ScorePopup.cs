using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Hide/shows the popup


public class ScorePopup : MonoBehaviour 
{
	//public Sprite starImageLine;
	public Sprite starImageFill;


	void Start() 
	{
	}


	void Update() 
	{
	}


	public void SetTitle(string text)
	{
		TextMeshProUGUI textTitle = transform.Find("Title").GetComponent<TextMeshProUGUI> ();
		textTitle.text = text;
	}


	public void SetMessage(string text)
	{
		TextMeshProUGUI textMessage = transform.Find("Message").GetComponent<TextMeshProUGUI> ();
		textMessage.text = text;
	}


	public void SetStars(int numStars)
	{
		Transform stars = transform.Find ("Stars");
		for (int i = 0; i < 5; i++) {
			if (i < numStars) {
				stars.GetChild (i).GetComponent<Image> ().sprite = starImageFill;
			}
		}
	}


	public void Show() 
	{
		this.gameObject.SetActive (true);
		// TODO: fadein
	}


	public void Hide()
	{
		this.gameObject.SetActive (false);
	}
}
