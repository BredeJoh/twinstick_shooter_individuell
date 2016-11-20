using UnityEngine;
using System.Collections;

public class charactermovement2 : MonoBehaviour {

    public float speed = 0f;
    //float rotationspeed = 2.0f;
    private CharacterController controller;
    public bool godmode = false;
    private bool autoFire = false;
    [HideInInspector]
    public Vector3 move;
    public static int damageMultiplyer = 1;
    public Weapon activeWeapon;
    public Weapon weapon1;
    public Weapon weapon2;
    public Weapon weapon3;
    public int shield = 1;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shield == 0)
            GetComponent<SpriteRenderer>().color = Color.blue;
        else
            GetComponent<SpriteRenderer>().color = Color.white;
        if (godmode)
            Health.playerHealth = 9999;
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        if (Input.GetButtonDown("Weapon1"))
            activeWeapon = weapon1;
        if (Input.GetButtonDown("Weapon2"))
            activeWeapon = weapon2;
        if (Input.GetButtonDown("Weapon3"))
            activeWeapon = weapon3;

        if (Input.GetKeyDown(KeyCode.Mouse1))
            autoFire = !autoFire;

        if (autoFire)
        {
            activeWeapon.firing = true;
            activeWeapon.fire();
        }

        else if (Input.GetKey(KeyCode.Mouse0) || Mathf.Abs(Input.GetAxis("rightJoystickVertical")) > 0.2f || Mathf.Abs(Input.GetAxis("rightJoystickHorizontal")) > 0.2f)
        {
            activeWeapon.firing = true;
            activeWeapon.fire();
        }
        else
            activeWeapon.firing = false;


        float vertical = Input.GetAxis("vertical");
        float horizontal = Input.GetAxis("horizontal");
        move = new Vector3(horizontal, -vertical, 0f);
        controller.Move(move * Time.deltaTime * speed);
        

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemylazer")
        {
            Health.playerHealth -= 1*shield;
            Points.combo = 1;
        }
        if (other.gameObject.tag == "enemy")
        {
            Health.playerHealth -= 3*shield;
            other.gameObject.transform.parent.GetComponent<Enemymovement>().health = 0;
            other.gameObject.transform.parent.GetComponent<Enemymovement>().Die();
            Points.score -= other.transform.parent.GetComponent<Enemymovement>().killValue * (Points.combo-1);
            Points.combo = 1;
            Destroy(other.transform.parent.gameObject);
        }
        if (Health.playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Health.playerHealth -= 3;
            Destroy(other.gameObject);
        }
        if (Health.playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
