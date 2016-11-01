﻿using UnityEngine;
using System.Collections;

public class Enemymovement : MonoBehaviour {
    private GameObject spiller;
    public Rigidbody rb;
    private SpriteRenderer sRendrer;
    public Transform enemylazerprefab;
    private float randomDistance;
    private float rotationSpeed;
    private float randomShootspeed;
    public float fart;
    [Range(0.5f, 3.0f)]
    public float moveDeadZone;
    public Vector3 dist;
    public Vector3 Rad;
    public float disty;
   // [HideInInspector]
    public int health;
    public int maxHealth = 100;
    public bool isKamikaze = false;
    float randomrotation;
    public pickups[] pickups;
    // Use this for initialization
    void Start() {

        health = maxHealth;
        sRendrer = GetComponent<SpriteRenderer>();
        rotationSpeed = Random.Range(3.0f, 4.5f);
        randomDistance = Random.Range(3.0f, 7.0f);
        randomrotation = Random.Range(-10f, 10f);
        randomShootspeed = Random.Range(2.0f, 4.0f);
        StartCoroutine(Shoot(randomShootspeed));
        spiller = GameObject.FindGameObjectWithTag("Player");
    }
    IEnumerator flash(float time)
    {
        yield return new WaitForSeconds(time);
        if (isKamikaze)
            sRendrer.color = Color.yellow;
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
    void dropPickup(pickups pickup)
    {
        if(Random.Range(1, 15) == 1)
            Instantiate(pickup, gameObject.transform.position, gameObject.transform.rotation);
    }
    void normalEnemy()
    {
       
        rotateAroundPlayer();
        if (dist.magnitude >= 10.0f)
            fart = 4.0f;
        else
            fart = 2.0f;
        if (dist.magnitude >= randomDistance + (moveDeadZone/2)) //stopper å bevege seg mot spiller når den er innenfor en viss distanse
        {
            moveTowards();
        }
        else if (dist.magnitude < (randomDistance - moveDeadZone))
        {
            moveAway();
        }
    }
    void Kamikaze()
    {      
        if (dist.magnitude >= 10.0f)
            fart = 4.0f;
        else
            fart = 2.0f;
        if (health >= (int)maxHealth / 2)
        {
            rotateAroundPlayer();
            if (dist.magnitude >= randomDistance + (moveDeadZone/2)) //stopper å bevege seg mot spiller når den er innenfor en viss distanse
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
        distfinder();
        if (health <= 0)
        {
            Points.score++;
            if(pickups.Length != 0)
                dropPickup(pickups[Random.Range(0, pickups.Length)]);
            Destroy(gameObject);
        }
        if (isKamikaze)
        {
            Kamikaze();
        }
        else normalEnemy();


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
        if (Health.playerHealth != 0)
        Instantiate(enemylazerprefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(WaitTime);
        StartCoroutine(Shoot(randomShootspeed));
        
    }
	
}
