using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Send message to game controller to pause/resume the game

public class PauseController : MonoBehaviour 
{

	void Start() 
	{		
	}


	void Update() 
	{
	}


	public void PauseGame()
	{
		gameObject.SendMessage ("Pause");
	}


	public void ResumeGame()
	{
		gameObject.SendMessage ("Resume");
	}
}
