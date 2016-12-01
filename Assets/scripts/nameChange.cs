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
}
