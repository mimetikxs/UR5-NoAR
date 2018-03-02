using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Attach this script to a button to reload the current scene


public class Replay : MonoBehaviour 
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
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
