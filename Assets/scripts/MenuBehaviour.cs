using UnityEngine;
using System.Collections;


public class MenuBehaviour : MonoBehaviour {

	//change to whatever input will be used for pausing
	private string pauseInput = "escape";
	private string pauseButton = "pause";

	//private string continueButton = "pause";
	private string quitButton = "Quit";
	//public gameobject for storing the pause menu
	public GameObject pauseImage;





	// Use this for initialization
	void Start () {
		pauseImage.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		Paused();
		PressToPause();
		PressToQuit();
	}

	//pauses and unpauses when escape is pressed
    void PressToPause() {
		if (Input.GetKeyDown(pauseInput) || Input.GetButtonDown(pauseButton)){
			
			if (!pauseImage.activeSelf) {
				pauseImage.SetActive(true);
			}
			else if (pauseImage.activeSelf) {
				pauseImage.SetActive(false);
			}
		}
	}

	//pauses game if the pause menu is active/visible
	void Paused() {
		if (pauseImage.activeSelf) {
			Time.timeScale = 0.0f;
		}
		else {
			Time.timeScale = 1.0f;
		}
	}


	//for buttons/keypresses to get out of the pause menu
	public void ClickToUnpause() {
		pauseImage.SetActive(false);
	}

	//for quitting the application/game
	public void ClickToQuit() {
		Application.Quit();
	}

	//for quitting using a button on a controller (when in the pause menu)
	private void PressToQuit() {
		if (pauseImage.activeSelf) {
			if (Input.GetButtonDown(quitButton)) {
				ClickToQuit();
			}
		}
	}
}
