using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	//name of level/scene to load
	public string levelName = "main";
	//inputs
	private string startButton = "pause";
	private string startKey = "space";

	void Update() {
		LoadLevelByName();
	}

	//loads level if start (controller) or a selected key is pressed
	void LoadLevelByName() {
		if (Input.GetButtonDown(startButton) || Input.GetKeyDown(startKey)) {
			SceneManager.LoadScene(levelName);
		}
	}
}
