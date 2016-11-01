using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    //Text txt;
    private Image healthbar;
    public static int playerHealth = 5;
    public static int maxPlayerHealth = 5;
    // Use this for initialization
    void Start () {
        //txt = gameObject.GetComponent<Text>();
        //txt.text = "Health : " + playerHealth;
		playerHealth = maxPlayerHealth;
        healthbar = gameObject.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        healthbar.fillAmount = (float)playerHealth / (float)maxPlayerHealth;
        //txt.text = "Health : " + playerHealth;
        if (playerHealth > maxPlayerHealth) playerHealth = maxPlayerHealth;
    }
}
