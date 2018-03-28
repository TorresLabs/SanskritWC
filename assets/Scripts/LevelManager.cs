using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private string gameLevel = "Game";

	public void GoTo (int versus)
	{
		PlayerPrefs.SetInt("MODE", versus);
		LoadLevel (gameLevel);
	}

	public void SetMode (int versus)
	{
		PlayerPrefs.SetInt("MODE", versus);
	}

	public void LoadLevel (string LevelName)
	{
		SceneManager.LoadScene(LevelName);
	}
}
