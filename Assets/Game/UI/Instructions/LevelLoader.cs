using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public bool useAR = false;

	private string scenesPath = "Scenes/";

	public void LoadLevel(string sceneName)
	{
		string sufix = useAR ? "_AR" : "";			
		SceneManager.LoadScene (scenesPath + sceneName + sufix);
	}
}
