using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public bool useAR = false;

	private string scenesPath = "Scenes/";

	public void LoadLevel(string sceneName)
	{
		string sufix = "";
		if (useAR) sufix = "_AR";
			
		SceneManager.LoadScene (scenesPath + sceneName + sufix);
	}
}
