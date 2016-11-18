using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class nameChange : MonoBehaviour
{

    public void OnNameChange(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();
    }
}
