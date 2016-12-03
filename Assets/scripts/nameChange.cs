using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class nameChange : MonoBehaviour
{

    public GameObject highscores;
    public GameObject Controlls;

    public void OnNameChange(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();
    }

    public void ShowHighscores()
    {
        if (highscores.activeSelf == false)
        {
            highscores.SetActive(true);
            for (int i = 0; i < highscores.transform.childCount; i++)
            {
                highscores.transform.GetChild(i).gameObject.SetActive(true);
            }
        }         
        else
            highscores.SetActive(false);

        if (Controlls.activeSelf == true)
            Controlls.SetActive(false);
    }
    public void ShowControlls()
    {
        if (Controlls.activeSelf == false)
            Controlls.SetActive(true);
        else
            Controlls.SetActive(false);

        if (highscores.activeSelf == true)
            highscores.SetActive(false);
    }
    public void ResetScores()
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
}
   