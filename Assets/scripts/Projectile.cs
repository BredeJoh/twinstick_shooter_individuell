using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private charactermovement2 player;
    [HideInInspector]
    public Weapon AssosiatedWeapon;
	private float spread;
	private float projectileSpeed;
	private float lifetime;
    [HideInInspector]
    public int damage;

    Vector3 playerVector;
    Vector3 velocity;

    // Use this for initialization
    void Start () {

		player = FindObjectOfType<charactermovement2> ();
        
		spread = player.activeWeapon.spread / 100;
		projectileSpeed = player.activeWeapon.projectileSpeed;
		lifetime = player.activeWeapon.projectileLifetime;

		Destroy (gameObject, lifetime);

		Vector3 aimPos = transform.position;

		Vector3 playerPos = player.transform.position;
		aimPos.x -= playerPos.x;
		aimPos.y -= playerPos.y;

		float angle = Mathf.Atan2 (aimPos.y, aimPos.x);
		//Debug.Log (angle);
		angle += Random.Range (-spread, spread);
		//Debug.Log (angle);

		float xVelocity = projectileSpeed * Mathf.Cos (angle);
		float yVelocity = projectileSpeed * Mathf.Sin (angle);

		//playerVector = player.GetComponent<Rigidbody2D> ().velocity;

		//GetComponent<Rigidbody2D> ().velocity = new Vector2 (xVelocity + ( playerVector.x * 0.75f ), yVelocity + ( playerVector.y * 0.75f ));
        velocity = new Vector3(xVelocity + ( 0.75f), yVelocity + ( 0.75f), 0f);
        //velocity = GetComponent<Rigidbody2D>().velocity;

    }

	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().velocity = velocity;
        
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //.GetIgnoreLayerCollision(9, 9);
        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Physics2D.GetIgnoreLayerCollision(9, 10);
        }

        if (other.gameObject.tag == "Boarder")
            Destroy(gameObject);

        if (other.gameObject.tag == "lazer")
        {
            Physics2D.GetIgnoreLayerCollision(9, 9);
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (other.gameObject.tag == "Wall") { 
            if (AssosiatedWeapon.WeaponLevel >= 5 || AssosiatedWeapon.weaponname == "Rifle")
            {
                foreach (ContactPoint2D contact in other.contacts)
                {
                    Vector3 normal = contact.normal;
                    velocity = Vector3.Reflect(velocity, normal);
                }
            }
            else Destroy(gameObject);
        }
    
        if (other.gameObject.tag == "enemy")
        {
            if (other.gameObject.GetComponent<Enemymovement>() != null)
            {
                int health = other.gameObject.GetComponent<Enemymovement>().health;
                other.gameObject.GetComponent<Enemymovement>().health -= damage * charactermovement2.damageMultiplyer;
                damage -= health;
            }   
			else
				other.gameObject.GetComponent<KamikazeScript> ().health -= damage * charactermovement2.damageMultiplyer;
            if(player.activeWeapon.name != "Rifle" || other.gameObject.GetComponent<Enemymovement>().health > 0)
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "enemy")
		{
			if (other.gameObject.GetComponent<Enemymovement> () != null)
				other.gameObject.GetComponent<Enemymovement> ().health -= damage;
			else
				other.gameObject.GetComponent<KamikazeScript> ().health -= damage;
            Destroy(gameObject);
		}
        if(other.gameObject.tag == "Wall" && AssosiatedWeapon.WeaponLevel < 5)
        {
            Destroy(gameObject);
        }
    }
}