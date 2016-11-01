using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	string levelToLoad = "main";
	string pauseButton = "pause";
	string quitButton = "Quit";
	public GameObject GameOverScreen;

	// Use this for initialization
	void Start () {
		GameOverScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		DeathCheck();
		RetryLevel();
	}

	//stop the game and display a game over screen
	void DeathCheck() {
		if (Health.playerHealth <= 0) {
			GameOverScreen.SetActive(true);
			Time.timeScale = 0f;
		}
	}

	void RetryLevel() {
		if (GameOverScreen.activeSelf) {
			if (Input.GetButtonDown(pauseButton)) {
				SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
			}
			if (Input.GetButtonDown(quitButton)) {
				Application.Quit();
			}
		}

	}
}
