using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Points : MonoBehaviour {

    Text txt;
    public static int combo = 1;
    int internalCombo = 1;
    public static int score = 0;
    public GameObject comboTimer;
    float timer;
    // Use this for initialization
	void Start () {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Score : " + score + "\n" + "Combo: " + combo;
		score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (internalCombo != combo)
        {
            comboTimer.GetComponent<Image>().fillAmount = 1f;
            internalCombo = combo;
        }
        if (comboTimer.GetComponent<Image>().fillAmount > 0 && combo > 1)
            comboTimer.GetComponent<Image>().fillAmount -= Time.deltaTime/1.75f;
        else
            combo = 1;
            
        txt.text = "Score : " + score + "\n" + "Combo: " + combo;
    }
}
