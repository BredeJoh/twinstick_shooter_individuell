using UnityEngine;
using System.Collections;

public class Wave_spawn : MonoBehaviour {

    public int waveNumber = 0;
    private float xLimit = 15f;
    private float yLimit = 7.5f;
    private float randomX;
    private float randomY;
    private float spawnSpeed = 1f; // var 2
    private GameObject randomEnemy;
    Vector3 spawnPoint = Vector3.zero;
    public Camera cam;
    Vector3 spawnInViewPort;
    private bool canSpawn = false;
    public int maxEnemies = 25;
    private int currentEnemies = 0;
    private int enemiesThisRound;
    private int enemiesTeller;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public Weapon weapon;
    int randomEnemyType;

    // Use this for initialization
    void Start () {
        waveNumber = 1;
        enemiesThisRound = 20; // 50
        StartCoroutine(spawn(spawnSpeed));
        randomEnemy = enemyPrefab;
        enemyPrefab.GetComponent<Enemymovement>().maxHealth = 50;
        enemyPrefab2.GetComponent<Enemymovement>().maxHealth = 50;
        enemyPrefab3.GetComponent<Enemymovement>().maxHealth = 100;
    }
	
	// Update is called once per frame
	void Update () {

        randomX = Random.Range(-xLimit, xLimit);
        randomY = Random.Range(-yLimit, yLimit);
        randomEnemyType = Random.Range(1, 11);
        if (randomEnemyType < 7)
            randomEnemy = enemyPrefab;
        else if (randomEnemyType > 6 && randomEnemyType < 10)
            randomEnemy = enemyPrefab2;
        else if (randomEnemyType > 9 && waveNumber > 2)
            randomEnemy = enemyPrefab3;

        spawnInViewPort = cam.WorldToViewportPoint(new Vector3(randomX, randomY, 0f));
        

        //print(spawnInViewPort.x + "    " + spawnInViewPort.y + "     " + spawnInViewPort.z);
        
        if ((spawnInViewPort.x > 1f || spawnInViewPort.x < 0f) || (spawnInViewPort.y > 1f || spawnInViewPort.y < 0f))
        {
            spawnPoint = new Vector3(randomX, randomY, 0f);
            canSpawn = true;
            
        }
        currentEnemies = gameObject.transform.childCount;
       
        if (enemiesTeller == enemiesThisRound && currentEnemies == 0)
        {
            nextWave();
        }
	}
    void nextWave()
    {
        waveNumber++;
        enemiesTeller = 0;
        if(maxEnemies < 50) // 30
            maxEnemies += 5; //10
        enemiesThisRound += 5 * waveNumber;
        enemyPrefab.GetComponent<Enemymovement>().maxHealth += 20;
        enemyPrefab2.GetComponent<Enemymovement>().maxHealth += 15;
        enemyPrefab3.GetComponent<Enemymovement>().maxHealth += 35;
        spawnSpeed -= 0.1f;
        if (spawnSpeed < 0.25) spawnSpeed = 0.25f;       // 0.35 
        if (waveNumber >= 10)
        {
            enemyPrefab.GetComponent<Enemymovement>().maxHealth += 20;
            enemyPrefab2.GetComponent<Enemymovement>().maxHealth += 15;
            enemyPrefab3.GetComponent<Enemymovement>().maxHealth += 35;
        }
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
        if (currentEnemies > maxEnemies/1.3f)
            StartCoroutine(spawn(spawnSpeed*1.4f));
        else
            StartCoroutine(spawn(spawnSpeed));

    }
    void OnDrawGizmos()
    {
        //Gizmos.DrawCube(spawnPoint, Vector3.one);
    }
}
