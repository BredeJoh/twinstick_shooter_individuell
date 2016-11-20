using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Points : MonoBehaviour {

    Text txt;
    public static int combo = 1;
    public static int score = 0;
	// Use this for initialization
	void Start () {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Score : " + score + "\n" + "Combo: " + combo;
		score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        txt.text = "Score : " + score + "\n" + "Combo: " + combo;
    }
}
