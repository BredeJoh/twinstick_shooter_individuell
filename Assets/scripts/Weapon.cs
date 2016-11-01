﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	private charactermovement2 player;
	private GameObject newProjectile;
    public GameObject projectile;
    public Transform firePoint;
    public int projectilesPerShot;
    public float spread;
	public float fireDelay;
	public float projectileLifetime;
	public float projectileSpeed;
    public int damage;
    [HideInInspector]
    public int WeaponLevel;
    public string LevelUpButton;
    private int currentWave;
    private Vector3 size;
    public Wave_spawn wavespawn;
    

    [HideInInspector]
    public static bool canLevelUp = false;
    [Header("Level Powerups")]
    [Range(-5.0f, 5.0f)]
    public float fireRatePerLevel;
    [Range(-5.0f, 5.0f)]
    public float spreadPerLevel;
    [Range(-5.0f, 5.0f)]
    public float projectileSpeedPerLevel;
    [Range(-1.0f, 1.0f)]
    public float ProjectileLifetimePerLevel;
    [Range(-0.5f, 0.5f)]
    public float projectileSizeIncrease;
    [Header("Level Powerups per 5 levels")]
    [Range(0, 5)]
    public int MoreProjectiles;
    [Range(0, 100)]
    public int damageIncrease;
	

	private bool canFire;

	// Use this for initialization
	void Start () {
        WeaponLevel = 1;
        currentWave = 1;
        size = Vector3.zero;
		canFire = true;
		player = FindObjectOfType<charactermovement2> ();
        projectile.GetComponent<Projectile>().damage = damage ;
	}
    void LevelUp()
    {
        WeaponLevel++;
        spread += spreadPerLevel;
        if(fireDelay > 0.02f)
            fireDelay = fireDelay - (fireRatePerLevel / 50f);
        projectileSpeed += projectileSpeedPerLevel;
        projectileLifetime += ProjectileLifetimePerLevel;
        size += new Vector3(projectileSizeIncrease, projectileSizeIncrease, 0f);
        if (WeaponLevel % 5 == 0)
        {
            projectilesPerShot += MoreProjectiles;
            damage += damageIncrease;
        }
        canLevelUp = false;
    }
	// Update is called once per frame
	void Update () {
        if (currentWave < wavespawn.waveNumber){
            currentWave = wavespawn.waveNumber;
            canLevelUp = true;
        }

        if (canLevelUp && Input.GetButtonDown(LevelUpButton))       
            LevelUp();     
	}
    
	public void fire(){
		if (canFire == false)
			return;
        for (int i = projectilesPerShot; i >= 1; i--)
        {
            newProjectile = Instantiate(projectile, firePoint.position, player.transform.rotation) as GameObject;
            newProjectile.transform.localScale += size;
        }
		StartCoroutine (Cooldown (fireDelay));
	}
    
    IEnumerator Cooldown(float fireDelay)
	{
		canFire = false;
		yield return new WaitForSeconds(fireDelay);
		canFire = true;
	}
}
