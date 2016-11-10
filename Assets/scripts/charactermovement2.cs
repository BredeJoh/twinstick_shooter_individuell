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

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
            Health.playerHealth--;
        }
        if (other.gameObject.tag == "enemy")
        {
            Health.playerHealth--;
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
            Health.playerHealth--;
            Destroy(other.gameObject);
        }
        if (Health.playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
