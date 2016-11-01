using UnityEngine;
using System.Collections;

public class characterMovement : MonoBehaviour {
    public float speed = 0f;
   // float zRot;
    //float angle;
    //float rotationspeed = 2.0f;
   
    private Rigidbody2D body2D;
    public float force = 0.5f;
    public float maxSpeed = 1f;
    public float drag;
    public bool godmode = false;
   // private bool isShooting = false;
	private bool autoFire = false;

    public Transform lazerprefab;
    public Weapon activeWeapon;
    public Weapon weapon1;
    public Weapon weapon2;
    public Weapon weapon3;


    // Use this for initialization
    void Start () {
        body2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (godmode)
            Health.playerHealth = 9999;
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        //angle = transform.rotation.eulerAngles.z;
        //zRot = Mathf.Deg2Rad*transform.rotation.eulerAngles.z;
        var vel = body2D.velocity;
        speed = vel.magnitude;


        if (Input.GetButtonDown("Weapon1"))
            activeWeapon = weapon1;
        if (Input.GetButtonDown("Weapon2"))
            activeWeapon = weapon2;
        if (Input.GetButtonDown("Weapon3"))
            activeWeapon = weapon3;

        //float rightvertical = Input.GetAxis("rightJoystickVertical");
        //float rightHorizontal = Input.GetAxis("rightJoystickHorizontal");
        //float abuttondown = Input.GetAxis("Fire1");

		if (Input.GetKeyDown (KeyCode.Mouse1))
			autoFire = !autoFire;

		if (autoFire)
			activeWeapon.fire ();
		else if (Input.GetKey (KeyCode.Mouse0) || Input.GetAxis("rightJoystickVertical") != 0f || Input.GetAxis("rightJoystickHorizontal") != 0f)
			activeWeapon.fire ();
        
        
        float vertical = Input.GetAxis("vertical");
        float horizontal = Input.GetAxis("horizontal");
        

        if (vertical < -0.2f && speed < maxSpeed)
        {
           // body2D.velocity += new Vector2(0, force);
            body2D.AddForce(new Vector2 (0, force), ForceMode2D.Impulse);
        }
        if (vertical > 0.2f && speed < maxSpeed)
        {
           // body2D.velocity += new Vector2(0, force*-1);
            body2D.AddForce(new Vector2(0, force*-1), ForceMode2D.Impulse);
        }
        if (horizontal > 0.2f && speed < maxSpeed)
        {
          //  body2D.velocity += new Vector2(force, 0);
            body2D.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
        }
        if (horizontal < -0.2f && speed < maxSpeed)
        {
          //  body2D.velocity += new Vector2(force*-1, 0);
            body2D.AddForce(new Vector2(force*-1, 0), ForceMode2D.Impulse);
        }
        if (Mathf.Abs(vertical) < 0.2f && Mathf.Abs(horizontal) < 0.2f)
        {
            GetComponent<Rigidbody2D>().drag = drag;
        }
        else
            GetComponent<Rigidbody2D>().drag = 1f;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemylazer")
        {
            Health.playerHealth--;
        }
        if (Health.playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Health.playerHealth--;
            Destroy(other.gameObject);
        }
        if (Health.playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    //IEnumerator attackAndWait(float WaitTime)
    //{
    //    isShooting = true;
    //    Instantiate(lazerprefab, transform.position, transform.rotation);
    //    yield return new WaitForSeconds(WaitTime);
    //    isShooting = false;
    //}
}

