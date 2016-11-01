using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class lazerMovement : MonoBehaviour {

    //GameObject body2D;
    public float speed = 1.0f;
    public float zRot;
    Vector3 move;
    
    public float speed2 = 0.1f;
    float angle;

    Vector3 joystickPosition;



    // Use this for initialization
    void Start () {
        StartCoroutine(Destroy(5f));

        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        float rightvertical = Input.GetAxis("rightJoystickVertical");
        float rightHorizontal = Input.GetAxis("rightJoystickHorizontal");

        joystickPosition = new Vector3(rightHorizontal, -rightvertical, 0f);

        //body2D = GameObject.FindGameObjectWithTag("Player");
        angle = Mathf.Sqrt((joystickPosition.x * joystickPosition.x) + (joystickPosition.y * joystickPosition.y));
        angle = Mathf.Atan2(joystickPosition.x, joystickPosition.y);
        if (angle < 0)
        {
            angle = Mathf.PI * 2 + angle;
        }
        angle = (angle * 360) / (Mathf.PI * 2);
        angle = 360 - angle;
        transform.eulerAngles = new Vector3(0f, 0f, angle);

    }
	
	// Update is called once per frame
	void Update () {

        //transform.position += move * speed;
        transform.position += joystickPosition.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {        
            Destroy(gameObject);
        }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        foreach (ContactPoint2D contact in other.contacts)
        {
            Vector2 normal = contact.normal;
            print(normal);
        }
    }
    IEnumerator Destroy(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        Destroy(gameObject);
    }

}
