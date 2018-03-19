using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public bool useAR = false;
	public bool loadInstructions = true;

	private string scenesPath = "Scenes/";

	public void LoadLevel(string sceneName)
	{
		if (loadInstructions) 
		{
			SceneManager.LoadScene (scenesPath + sceneName + "_UI_intro");
		} 
		else 
		{
			string sufix = useAR ? "_AR" : "";			
			SceneManager.LoadScene (scenesPath + sceneName + sufix);
		}

	}
}
