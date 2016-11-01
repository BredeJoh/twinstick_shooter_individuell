using UnityEngine;
using System.Collections;

public class CameraTargetController : MonoBehaviour {

	public GameObject player;
	public float maxCameraDistance;
	public float cameraSpeed;
    public Transform border1;
    public Transform border2;

	Vector3 mousePos;
	Vector3 worldPos;

	public bool inputType;
	public static bool JOYSTICK = true;
	public static bool MOUSE = false;

	// Use this for initialization
	void Start () {

		inputType = JOYSTICK;

	}
	
	// Update is called once per frame


	void Update () {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            		inputType = MOUSE;
            
            	if (Input.GetAxisRaw ("Mouse X") != 0f || Input.GetAxisRaw ("Mouse Y") != 0f)
            		inputType = MOUSE;
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleControlScheme();

        if (Input.GetAxis("rightJoystickVertical") != 0 || Input.GetAxis("rightJoystickHorizontal") != 0)
            inputType = JOYSTICK;
        Vector3 playerPos = player.transform.position;
        
        if (inputType == MOUSE) {

			//Track Mouse Position
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);

			//Mouse Position compared to Player Avatar
			Vector3 cameraPos = Camera.main.transform.position;
			mousePos.Set (mousePos.x - cameraPos.x, mousePos.y - cameraPos.y, -10);

			float angle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg;
			float dist = Mathf.Sqrt (Mathf.Pow (mousePos.x, 2) + Mathf.Pow (mousePos.y, 2));

			float distx = dist * Mathf.Cos (angle * Mathf.Deg2Rad);
			float disty = dist * Mathf.Sin (angle * Mathf.Deg2Rad);

			Vector3 temp = new Vector3 (distx, disty, -10);

			//Move Camera Target
			transform.position = new Vector3(playerPos.x + temp.x / 4, playerPos.y + temp.y / 4, -10);

		} else {
			
			//Controller
			float joyVertical = Input.GetAxis ("rightJoystickVertical");
			float joyHorizontal = Input.GetAxis ("rightJoystickHorizontal");

			transform.position = new Vector3 (playerPos.x + maxCameraDistance * joyHorizontal, playerPos.y + maxCameraDistance * -joyVertical, -10);
		}
        if (transform.position.x < border1.position.x)
        {
            transform.position = new Vector3(border1.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y > border1.position.y)
        {
            transform.position = new Vector3(transform.position.x, border1.transform.position.y, transform.position.z);
        }
        if (transform.position.x > border2.position.x)
        {
            transform.position = new Vector3(border2.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y < border2.position.y)
        {
            transform.position = new Vector3(transform.position.x, border2.transform.position.y, transform.position.z);
        }
        Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, transform.position, cameraSpeed * Time.deltaTime);
	}

	public void ToggleControlScheme(){
		inputType = !inputType;
	}
		
}
