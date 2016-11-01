using UnityEngine;
using System.Collections;

public class CharacterRotation : MonoBehaviour {

	private CameraTargetController camTarget;

	private Vector3 mousePos;
	private Vector3 pivotPos;
    [HideInInspector]
	public float angle;

	public static bool JOYSTICK = true;
	public static bool MOUSE = false;

	// Use this for initialization
	void Start () {
		camTarget = FindObjectOfType<CameraTargetController> ();
	}	
	
	// Update is called once per frame
	void FixedUpdate () {

		if (camTarget.inputType == MOUSE) 
		{
			//Mouse
			mousePos = Input.mousePosition;
			mousePos.z = 5f;

			pivotPos = Camera.main.WorldToScreenPoint (transform.position);
			mousePos.x -= pivotPos.x;
			mousePos.y -= pivotPos.y;

			angle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg;

		} else {

			//Controller
            
			float joyVertical = Input.GetAxis ("rightJoystickVertical");
			float joyHorizontal = Input.GetAxis ("rightJoystickHorizontal");
            if (joyVertical != 0f || joyHorizontal != 0f)
			    angle = Mathf.Atan2 (-joyVertical, joyHorizontal) * Mathf.Rad2Deg;

		}

		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));

	}
}
