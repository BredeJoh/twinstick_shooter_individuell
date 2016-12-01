using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class highscores : MonoBehaviour {
    //public GameObject player;
    public Text[] Scores;
    private string playerName;
	// Use this for initialization
	void Start () {
        
        playerName = PlayerPrefs.GetString("PlayerName");
        if (PlayerPrefs.GetInt("highscore" + 0) == 0)
        {
            PlayerPrefs.SetInt("highscore" + 0, 100000);
            PlayerPrefs.SetInt("highscore" + 1, 50000);
            PlayerPrefs.SetInt("highscore" + 2, 25000);
            PlayerPrefs.SetInt("highscore" + 3, 10000);
            PlayerPrefs.SetInt("highscore" + 4, 7500);
            PlayerPrefs.SetInt("highscore" + 5, 6677);
            PlayerPrefs.SetInt("highscore" + 6, 6000);
            PlayerPrefs.SetInt("highscore" + 7, 5000);
            PlayerPrefs.SetInt("highscore" + 8, 2500);
            PlayerPrefs.SetInt("highscore" + 9, 1000);
            PlayerPrefs.SetString("highscore name" + 0, "Git gud");
            PlayerPrefs.SetString("highscore name" + 1, "Peter");
            PlayerPrefs.SetString("highscore name" + 2, "John");
            PlayerPrefs.SetString("highscore name" + 3, "N00B");
            PlayerPrefs.SetString("highscore name" + 4, "bot#12");
            PlayerPrefs.SetString("highscore name" + 5, "bot#42");
            PlayerPrefs.SetString("highscore name" + 6, "l33tGam3r");
            PlayerPrefs.SetString("highscore name" + 7, "bot#1");
            PlayerPrefs.SetString("highscore name" + 8, "l2p");
            PlayerPrefs.SetString("highscore name" + 9, "scrub");
            PlayerPrefs.Save();
        }
        UpdateText();
    }
	
	// Update is called once per frame
	void Update () {
	    if ((Health.playerHealth <= 0))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            SetHighScore(Points.score, playerName);
            UpdateText();
            Points.score = 0;
        }
	}
    void SetHighScore(int score, string name)
    {
        for (int i = 0; i < 10; i++)
        {
            if (score > PlayerPrefs.GetInt("highscore" + i))
            {
                SetHighScore(PlayerPrefs.GetInt("highscore" + i), PlayerPrefs.GetString("highscore name" + i));
                PlayerPrefs.SetInt("highscore" + i, score);
                PlayerPrefs.SetString("highscore name" + i, name);
                PlayerPrefs.Save();
                i = 10;               
            }  
        }
    }
    void UpdateText()
    {
        for (int i = 0; i < 10; i++)
        {
           // if (i == 1)
                Scores[i].text = PlayerPrefs.GetString("highscore name" + i) + ": " + PlayerPrefs.GetInt("highscore" + i);
           // else if (i == 2)
           //     Scores[i].text = i+1 + "nd: " + PlayerPrefs.GetInt("highscore" + i);
           // else if (i == 3)
           //     Scores[i].text = i+1 + "rd: " + PlayerPrefs.GetInt("highscore" + i);
           // else
           //     Scores[i].text = i+1 + "th: " + PlayerPrefs.GetInt("highscore" + i);
        }
    }

}
