using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Attach this script to a button to load the main menu on click


public class GoToMainMenu : MonoBehaviour 
{
	private Button btn;


	private void Awake()
	{
		btn = this.GetComponent<Button> ();
	}


	private void OnEnable() 
	{
		btn.onClick.AddListener (Go);
	}


	private void OnDisable ()
	{
		btn.onClick.RemoveListener (Go);
	}
		

	private void Go ()
	{
		SceneManager.LoadScene ("Scenes/MainMenu");
	}
}
