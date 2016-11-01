using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class weapon_display : MonoBehaviour {

    public GameObject[] weaponPrefabs;
    public GameObject[] buttons;
    public string[] activateButtons;
    public Text[] displayTexts;
    private bool[] isActive = new bool[3];


    // Use this for initialization
    void Start() {
        for (int i = 0; i < 3; i++)
        {
            isActive[i] = false;
            displayTexts[i] = displayTexts[i].GetComponent<Text>();
        }
        isActive[0] = true;
    }
	
	// Update is called once per frame
	void Update () {
        TextChange();
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetButtonDown(activateButtons[i]))
            {
                for (int a = 0; a < 3; a++)
                {
                    isActive[a] = false;
                    isActive[i] = true;
                }
            }
            if (isActive[i])
                buttons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            else
                buttons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
            if(Weapon.canLevelUp == true)
            {
                buttons[i].GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
            }
        }      
	}
    void TextChange()
    {
        for (int i = 0; i < 3; i++)
            displayTexts[i].text = "Lv: " + weaponPrefabs[i].GetComponent<Weapon>().WeaponLevel;
    }
    
}
