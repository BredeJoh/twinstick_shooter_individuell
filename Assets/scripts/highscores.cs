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
        PlayerPrefs.SetInt("highscore" + 0, 0);
        if (PlayerPrefs.GetInt("highscore" + 0) == 0)
        {
            PlayerPrefs.SetInt("highscore" + 0, 500);
            PlayerPrefs.SetInt("highscore" + 1, 250);
            PlayerPrefs.SetInt("highscore" + 2, 225);
            PlayerPrefs.SetInt("highscore" + 3, 200);
            PlayerPrefs.SetInt("highscore" + 4, 175);
            PlayerPrefs.SetInt("highscore" + 5, 150);
            PlayerPrefs.SetInt("highscore" + 6, 125);
            PlayerPrefs.SetInt("highscore" + 7, 100);
            PlayerPrefs.SetInt("highscore" + 8, 25);
            PlayerPrefs.SetInt("highscore" + 9, 10);
            PlayerPrefs.SetString("highscore name" + 0, "Bob");
            PlayerPrefs.SetString("highscore name" + 1, "Peter");
            PlayerPrefs.SetString("highscore name" + 2, "John");
            PlayerPrefs.SetString("highscore name" + 3, "N00B");
            PlayerPrefs.SetString("highscore name" + 4, "Brede");
            PlayerPrefs.SetString("highscore name" + 5, "Martin");
            PlayerPrefs.SetString("highscore name" + 6, "Hei");
            PlayerPrefs.SetString("highscore name" + 7, "Mamma");
            PlayerPrefs.SetString("highscore name" + 8, "Pappa");
            PlayerPrefs.SetString("highscore name" + 9, "Kennet");
            PlayerPrefs.Save();
        }
        UpdateText();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Health.playerHealth <= 0 && Points.score > 0)
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
