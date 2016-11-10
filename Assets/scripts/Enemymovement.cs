﻿using UnityEngine;
using System.Collections;

public class Enemymovement : MonoBehaviour {
    private GameObject spiller;
    public Rigidbody rb;
    public GameObject deathEffect;
    private SpriteRenderer sRendrer;
    public Transform enemylazerprefab;
    private float randomDistance;
    private float rotationSpeed;
    private float randomShootspeed;
    public float fart;
    public GameObject firepoint;
    public int ProjectilesPerShot = 1;
    [Range(0.5f, 3.0f)]
    public float moveDeadZone;
    public Vector3 dist;
    public Vector3 Rad;
    public float disty;
    [HideInInspector]
    public int health;
    public int maxHealth = 100;
    public bool isKamikaze = false;
    public bool isTank = false;
    float randomrotation;
    public pickups[] pickups;
    // Use this for initialization
    public GameObject deathsound;
    public GameObject fire;
    void Start() {
        health = maxHealth;
        sRendrer = GetComponent<SpriteRenderer>();
        rotationSpeed = Random.Range(3.0f, 4.5f);
        randomDistance = Random.Range(3.0f, 7.0f);
        randomrotation = Random.Range(-10f, 10f);
        randomShootspeed = Random.Range(5.0f, 10.0f);
        spiller = GameObject.FindGameObjectWithTag("Player");
        LookAtPlayer();
        distfinder();
        StartCoroutine(Shoot(randomShootspeed));
    }
    IEnumerator flash(float time)
    {
        yield return new WaitForSeconds(time);
        if (isKamikaze)
            sRendrer.color = Color.yellow;
        else if (isTank)
            sRendrer.color = Color.green;
        else
            sRendrer.color = Color.red;
    }
    void distfinder() //finner distanse mellom spiller og enemy
    {
            dist = spiller.transform.position - gameObject.transform.position;        
    }
    void moveTowards()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, spiller.transform.position, Time.deltaTime * fart);
    }
    void moveAway()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, spiller.transform.position, Time.deltaTime * -fart);
    }
    void rotateAroundPlayer()
    {
        if (randomrotation > 0)
            transform.RotateAround(spiller.transform.position, Vector3.forward, 5 * rotationSpeed * Time.deltaTime);
        else
            transform.RotateAround(spiller.transform.position, Vector3.back, 5 * rotationSpeed * Time.deltaTime);
    }
    void LookAtPlayer()
    {
        Vector3 enemyToPlayer = new Vector3(0f, 0f, 0f);
        GameObject body2D = GameObject.FindGameObjectWithTag("Player");
        enemyToPlayer = body2D.transform.position - transform.position;
        float angle = Mathf.Sqrt((enemyToPlayer.x * enemyToPlayer.x) + (enemyToPlayer.y * enemyToPlayer.y));
        angle = Mathf.Atan2(enemyToPlayer.x, enemyToPlayer.y);
        if (angle < 0)
        {
            angle = Mathf.PI * 2 + angle;
        }
        angle = (angle * 360) / (Mathf.PI * 2);
        angle = 360 - angle;
        transform.eulerAngles = new Vector3(0f, 0f, angle);

    }
    void dropPickup(pickups pickup)
    {
        if(Random.Range(1, 25) == 1)
            Instantiate(pickup, gameObject.transform.position, gameObject.transform.rotation);
    }

    void Die()
    {
        if (!isTank || runtimes > 1)
        {
            Instantiate(deathsound);
            Points.score++;
            if (pickups.Length != 0)
                dropPickup(pickups[Random.Range(0, pickups.Length)]);
            GameObject effect = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation) as GameObject;
            effect.SetActive(true);
            effect.transform.parent = null;
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }
    }
    void normalEnemy() // Basic AI for basefienden
    {
        if (health <= 0)
            Die();
        rotateAroundPlayer();
        if (dist.magnitude >= 10.0f)
            fart = 4.0f;
        else
            fart = 2.0f;
        if (dist.magnitude >= randomDistance + (moveDeadZone/2)) 
        {
            moveTowards();
        }
        else if (dist.magnitude < (randomDistance - moveDeadZone))
        {
            moveAway();
        }
    }
    void Tank()
    {
        if (health <= 0 && runtimes == 0)
            StartCoroutine(ShootManyOnce(9));
        normalEnemy();
    }
    void Kamikaze() // basic Ai for kamikaze fienden
    {
        if (health <= 0)
            Die();
        if (dist.magnitude >= 10.0f)
            fart = 4.0f;
        else
            fart = 2.0f;
        if (health >= (int)maxHealth / 2)
        {
            rotateAroundPlayer();
            if (dist.magnitude >= randomDistance + (moveDeadZone/2)) 
            {
                moveTowards();
            }
            else if (dist.magnitude < (randomDistance - moveDeadZone))
            {
                moveAway();
            }
        }
        else if (health <= (int)maxHealth / 2)
        {
            fart = 5.0f;
            moveTowards();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtPlayer();
        distfinder();
        if (isKamikaze)
            Kamikaze();
        else if (isTank)
            Tank();
        else
            normalEnemy();
        


    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "lazer")
        {
			sRendrer.color = Color.white;
			StartCoroutine (flash (0.2f));
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "lazer")
        {
            sRendrer.color = Color.white;
            StartCoroutine(flash(0.2f));
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    IEnumerator Shoot(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        if (Health.playerHealth != 0)
        {
            GameObject effect = Instantiate(fire, transform.position, transform.rotation) as GameObject;
            fire.SetActive(true);
            fire.transform.parent = null;
            float angle = 10;
            for (int i = 0, x = 0; i < ProjectilesPerShot; i++)
            {
                print(transform.rotation.z);
                if (i == 0)
                    Instantiate(enemylazerprefab, firepoint.transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + (i * angle))));
                else if (i % 2 != 0)
                {
                    x++;
                    Instantiate(enemylazerprefab, firepoint.transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + (x * angle))));
                }
                else
                    Instantiate(enemylazerprefab, firepoint.transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + (x * -angle))));
            }
        }
        
        StartCoroutine(Shoot(randomShootspeed));
        
    }
    int runtimes = 0;
    IEnumerator ShootManyOnce(int projectileCount)
    {
        runtimes++;
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.GetComponent<CircleCollider2D>().enabled = false;
        float angle = 40;
        for (int i = 0, x = 0; i < projectileCount; i++)
        {
            print(transform.rotation.z);
            if (i == 0)
                Instantiate(enemylazerprefab, firepoint.transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + (i * angle))));
            else if (i % 2 != 0)
            {
                x++;
                Instantiate(enemylazerprefab, firepoint.transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + (x * angle))));
            }
            else
                Instantiate(enemylazerprefab, firepoint.transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + (x * -angle))));
                yield return new WaitForEndOfFrame();
        }
        runtimes++;
        yield break;
    }
}
