using UnityEngine;
using System.Collections;

public class pickups : MonoBehaviour {

    [Header("Permanent Bonuses")]
    [Range(0, 5)]
    public int MaxHealthIncrease;
    [Range(0, 5)]
    public int HealthIncrease;

    [Header("x Sec Bonuses")]
    [Range(0f, 60f)]
    public float BonusTime;
    [Range(1, 5)]
    public int DamageIncrease = 1;
    [Range(-5f, 5f)]
    public float speedIncrease;
    [Range(1, 5)]
    public int fireRateMultiplier;

    private GameObject player;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator isPickedUpReset(float waitTime)
    {
        Health.maxPlayerHealth += MaxHealthIncrease;
        Health.playerHealth += HealthIncrease;
        charactermovement2.damageMultiplyer *= DamageIncrease;
        player.GetComponent<charactermovement2>().speed += speedIncrease;
        player.GetComponent<charactermovement2>().weapon1.fireDelay /= fireRateMultiplier;
        player.GetComponent<charactermovement2>().weapon2.fireDelay /= fireRateMultiplier;
        player.GetComponent<charactermovement2>().weapon3.fireDelay /= fireRateMultiplier;

        yield return new WaitForSeconds(waitTime);

        charactermovement2.damageMultiplyer /= DamageIncrease;
        player.GetComponent<charactermovement2>().speed -= speedIncrease;
        player.GetComponent<charactermovement2>().weapon1.fireDelay *= fireRateMultiplier;
        player.GetComponent<charactermovement2>().weapon2.fireDelay *= fireRateMultiplier;
        player.GetComponent<charactermovement2>().weapon3.fireDelay *= fireRateMultiplier;

        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            print("hei");
            StartCoroutine(isPickedUpReset(BonusTime));
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
}
