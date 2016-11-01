using UnityEngine;
using System.Collections;

public class KamikazeScript : MonoBehaviour {

	private GameObject spiller;
	//private GameObject fiende;
	public Rigidbody rb;
	private SpriteRenderer sRendrer;
	public Transform enemylazerprefab;
	private float a;
	private float b;
	private float r;
	private float rs;
	public float fart;
	public float amp;
	public Vector3 dist;
	public Vector3 edist;
	public Vector3 Rad;
	public float disty;
    [HideInInspector]
    public int health;
    public int maxHealth = 100;
	float randomrotation;
	// Use this for initialization
	void Start () {
        //        if (transform.position.y == -32)
        //        {
        //            speed = -1.0f;
        //        }
        health = maxHealth;
		sRendrer = GetComponent<SpriteRenderer> ();
		r = Random.Range(4.0f, 8f);
		rs = Random.Range(3.0f, 7.0f);
		randomrotation = Random.Range(-10f, 10f);
		StartCoroutine(Shoot(rs));
		spiller = GameObject.FindGameObjectWithTag("Player");
		//fiende = GameObject.FindGameObjectWithTag("enemy");
	}
	IEnumerator flash(float time)
	{
		yield return new WaitForSeconds (time);
		sRendrer.color = Color.yellow;
	}
	void distfinder() //finner distanse mellom spiller og enemy
	{

		dist = spiller.transform.position - transform.position;
		//edist = fiende.transform.position - transform.position;
	}
	void moveTowards()
	{
		gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, spiller.transform.position , Time.deltaTime * fart);
	}
	void moveAway()
	{
		gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, spiller.transform.position, Time.deltaTime * -fart);
	}
	void rotateAroundPlayer()
	{
		if (randomrotation > 0)
			transform.RotateAround(spiller.transform.position, Vector3.forward, 5 * r * Time.deltaTime);
		else
			transform.RotateAround(spiller.transform.position, Vector3.back, 5 * r * Time.deltaTime);
	}
	// Update is called once per frame
	void FixedUpdate()
	{
        if (health <= 0)
        {
            Points.score++;
            Destroy(gameObject);
        }
        a = 4f;
		b = 2f;
		amp = (a * Mathf.Log (b * (dist.magnitude + 1f)));
		distfinder ();
		if (dist.magnitude >= 10.0f) {
			fart = 2.0f;
		}
		if (health >= (int)maxHealth/2) {
			rotateAroundPlayer ();
			if (dist.magnitude < (r - 1f)) {
				moveAway ();
			}
		}
		else if (health <= (int)maxHealth/2) 
		{
			r = 0;
            fart = 0.1f + amp;
            fart = Mathf.Clamp(fart, 0.0f, 6f);
        }

		if (dist.magnitude >= r || health <= ((int)maxHealth / 1.5)) { //stopper å bevege seg mot spiller når den er innenfor en viss distanse
			moveTowards ();
		}

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
            StartCoroutine(flash(0.2f));
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "lazer")
        {
            sRendrer.color = Color.white;
            StartCoroutine(flash(0.2f));
        }
    }
    IEnumerator Shoot(float WaitTime)
	{
		if (Health.playerHealth != 0)
			Instantiate(enemylazerprefab, transform.position, transform.rotation);
		yield return new WaitForSeconds(WaitTime);
		StartCoroutine(Shoot(rs));

	}
}
