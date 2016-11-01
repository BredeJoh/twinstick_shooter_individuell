using UnityEngine;
using System.Collections;

public class Wave_spawn : MonoBehaviour {

    public int waveNumber = 0;
    private float xLimit = 15f;
    private float yLimit = 7.5f;
    private float randomX;
    private float randomY;
    private float spawnSpeed = 2.0f;
    private GameObject randomEnemy;
    Vector3 spawnPoint = Vector3.zero;
    public Camera cam;
    Vector3 spawnInViewPort;
    private bool canSpawn = false;
    public int maxEnemies = 20;
    private int currentEnemies = 0;
    private int enemiesThisRound;
    private int enemiesTeller;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public Weapon weapon;

    // Use this for initialization
    void Start () {
        
        waveNumber = 1;
        enemiesThisRound = 10;
        StartCoroutine(spawn(2f));
        randomEnemy = enemyPrefab;
        enemyPrefab.GetComponent<Enemymovement>().maxHealth = 100;
        enemyPrefab2.GetComponent<Enemymovement>().maxHealth = 75;

    }
	
	// Update is called once per frame
	void Update () {

        randomX = Random.Range(-xLimit, xLimit);
        randomY = Random.Range(-yLimit, yLimit);
        if (randomEnemy == enemyPrefab)
            randomEnemy = enemyPrefab2;
        else if (randomEnemy == enemyPrefab2)
            randomEnemy = enemyPrefab3;
        else if (randomEnemy == enemyPrefab3)
            randomEnemy = enemyPrefab;

        spawnInViewPort = cam.WorldToViewportPoint(new Vector3(randomX, randomY, 0f));
        

        //print(spawnInViewPort.x + "    " + spawnInViewPort.y + "     " + spawnInViewPort.z);

        if ((spawnInViewPort.x > 1f || spawnInViewPort.x < 0f) && (spawnInViewPort.y > 1f || spawnInViewPort.y < 0f))
        {
            spawnPoint = new Vector3(randomX, randomY, 0f);
            canSpawn = true;
            
        }
        currentEnemies = gameObject.transform.childCount;
        

        if (enemiesTeller == enemiesThisRound && currentEnemies == 0)
        {
            nextWave();
            print(waveNumber);
        }
	}
    void nextWave()
    {
        print(spawnSpeed);
        waveNumber++;
        enemiesTeller = 0;
        if(maxEnemies < 30)
            maxEnemies = 10 + (waveNumber*3);
        enemiesThisRound = 5 * waveNumber + 5;
        enemyPrefab.GetComponent<Enemymovement>().maxHealth += 35;
        enemyPrefab2.GetComponent<Enemymovement>().maxHealth += 20;
        spawnSpeed -= 0.2f;
        if (spawnSpeed < 0.35) spawnSpeed = 0.35f;       
    }
    IEnumerator spawn(float WaitTime)
    {
        if (canSpawn && currentEnemies < maxEnemies && enemiesThisRound != enemiesTeller)
        {
            if (waveNumber == 1)
            {
                GameObject enemyspawning = Instantiate(enemyPrefab, spawnPoint, transform.rotation) as GameObject;
                enemyspawning.transform.parent = gameObject.transform;
            }
            else
            {
                GameObject enemyspawning = Instantiate(randomEnemy, spawnPoint, transform.rotation) as GameObject;
                enemyspawning.transform.parent = gameObject.transform;
            }
            enemiesTeller++;
            canSpawn = false;
        }
        yield return new WaitForSeconds(WaitTime);
        StartCoroutine(spawn(spawnSpeed));

    }
}
