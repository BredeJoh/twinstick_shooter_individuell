using UnityEngine;
using System.Collections;

public class enemylazermovement : MonoBehaviour {

    GameObject body2D;
    private float speed = 0.10f;
    float angle;
    Vector3 enemyToPlayer;
    

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Destroy(5f));

        enemyToPlayer = new Vector3(0f, 0f, 0f);
        body2D = GameObject.FindGameObjectWithTag("Player");
        enemyToPlayer = body2D.transform.position - transform.position;
        angle = Mathf.Sqrt((enemyToPlayer.x * enemyToPlayer.x) + (enemyToPlayer.y * enemyToPlayer.y));
        angle = Mathf.Atan2(enemyToPlayer.x, enemyToPlayer.y);
        if (angle < 0)
        {
            angle = Mathf.PI*2 + angle;
        }
        angle = (angle * 360)/(Mathf.PI*2);
        angle = 360 - angle;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    } 

    // Update is called once per frame
    void Update()
    {
        transform.position += enemyToPlayer.normalized * speed;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Destroy(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        Destroy(gameObject);
    }
}
