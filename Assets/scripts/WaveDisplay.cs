using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveDisplay : MonoBehaviour {


	//text displaying wave number in pause menu
	Text DisplayedWave;
    public Wave_spawn wavespawn;
    


	// Use this for initialization
	void Start () {
		DisplayedWave = GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
		WavePlayerDisplay(wavespawn.waveNumber);
	}
		
	//changes wave number in the pause menu so the player can see which wave they are on
	void WavePlayerDisplay(int currentWave) {
		DisplayedWave.text = "WAVE " + currentWave;
	}
}
