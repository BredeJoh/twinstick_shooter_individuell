using UnityEngine;
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
    public bool firing = false;
    

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
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
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
        //projectile.GetComponent<TrailRenderer>().startWidth += projectileSizeIncrease;
        //projectile.GetComponent<TrailRenderer>().endWidth += projectileSizeIncrease/2;
        if (WeaponLevel % 5 == 0)
        {
            projectilesPerShot += MoreProjectiles;
            damage += damageIncrease;
        }
        canLevelUp = false;
    }
	// Update is called once per frame
	void Update () {
        if (gameObject.name == "Flamethrower" && !firing)
            audioSource.Stop();
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
        if (gameObject.name != "Flamethrower")
            audioSource.PlayOneShot(audioSource.clip, 1f);
        else if (firing && !audioSource.isPlaying)
            audioSource.Play();

        for (int i = projectilesPerShot; i >= 1; i--)
        {
            newProjectile = Instantiate(projectile, firePoint.position, player.transform.rotation) as GameObject;
            newProjectile.transform.localScale += size;
            newProjectile.GetComponent<TrailRenderer>().startWidth += size.x;
            newProjectile.GetComponent<TrailRenderer>().endWidth += size.y / 4;
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
