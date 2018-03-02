using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	private string scenesPath = "Scenes/Javi/";

	public void LoadLevel(string sceneName)
	{
		SceneManager.LoadScene (scenesPath + sceneName);
	}
}
