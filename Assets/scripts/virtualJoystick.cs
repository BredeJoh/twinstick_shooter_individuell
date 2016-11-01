using UnityEngine;
using System.Collections;

public class virtualJoystick : MonoBehaviour {


    public Transform body;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        
        float rightvertical = Input.GetAxis("rightJoystickVertical");
        float rightHorizontal = Input.GetAxis("rightJoystickHorizontal");
        gameObject.transform.position = body.transform.position + new Vector3(rightHorizontal, -rightvertical, 0f)* 5f;


	}
}
