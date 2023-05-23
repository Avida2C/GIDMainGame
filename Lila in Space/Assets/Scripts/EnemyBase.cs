using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //Set the particle system
    [Header("Unity Setup")] public ParticleSystem DeathParticleSystem;
    
    //Enemy speed
    [SerializeField]
    public float enemyVelocity = 1f;
    //Enemy time to upgrade
    [SerializeField]
    public float enemyUpgradeTime;
    //Maximum enemy health
    [SerializeField]
    public int maximumHealth;
    
    //PowerUp drop for invincibility
    [SerializeField]
    private GameObject powerupInvincible;
    //PowerUp drop for shooting upgrades
    [SerializeField]
    private GameObject powerUpShoot;
    //PowerUp drop for clearing screen from enemy gameobjects
    [SerializeField]
    private GameObject powerupNuke;
    //PowerUp drop for player lives
    [SerializeField]
    private GameObject powerupHealth;

    //AudioSource to enable audioclips
    [HideInInspector]
    public AudioSource audioProperties;
    //To attach AudioClip
    [SerializeField]
    private AudioClip Dead;

    //Component for the GameController
    [HideInInspector]
    public GameController gameController;

    //Enemy Health
    [HideInInspector]
    public Health health;

    //Enemy movement boundaries
    public float boundsLowX = -8.8f;
    public float boundsHighX = 8.8f;
    public float boundsLowY = -2.0f;
    public float boundsHighY = 4.0f;

    //Enemy Collider
    private BoxCollider2D colliderOnSpawn;

    //The sprite of enemy
    [HideInInspector]
    public SpriteRenderer sprite;

    //Player from the PlayerControl script
    [HideInInspector]
    public PlayerControl player;

    //Enemy not in playing area
    [HideInInspector]
    public bool newSpawn = true;

    // Start is called before the first frame update
    public void Start()
    {
        //Get the audioSource Component with the tag "audioSource" 
        audioProperties = GameObject.FindWithTag("audioSource").GetComponent<AudioSource>();
       
        //Sprite Renderer
        sprite = GetComponent<SpriteRenderer>();
        
        //Enemy Health 
        health = GetComponent<Health>();
        
        //Player GameObject from PlayerControl
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        
        //GameController 
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        //Maximum health of the enemy is set to 1
        health.MaximumHealth = 1;

    }


    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        //if enemy is hit by the player projectile
        if (collider.tag == "Projectile" && this.health != null)
        {
            //Remove 1 health from the enemy
            this.health.Decrement();
            //check if the enemy is dead by going to the Method IsDead
            if (this.health.IsDead())
            {
                //Play the audioclip found in the "Dead" AudioSource
                audioProperties.PlayOneShot(Dead);

                //Check whether a powerup is dropped
                this.Drops();
                
                //Add score on enemy death
                player.AddKill();
                
                //Particle system effect
                Instantiate(DeathParticleSystem, transform.position, Quaternion.identity);
                
                //Enemy gameObject is Destroyed
                Destroy(gameObject);
                
            }

        }
        else if(collider.tag == "Player")
        {
            //Enemy is destroyed
            Destroy(gameObject);
        }
        else if (collider.tag == "Boundary")
        {
            this.newSpawn = false;
        }
    }
    /// <summary>
    /// Used to determine the powerup drop rates for each powerup type 
    /// enemy drops the powerup if the number needed is generated
    /// </summary>
    public void Drops()
    {
        //5%
        int random = Random.Range(1, 30);
        if (random == 4)
        {

            Instantiate(powerupNuke, transform.position, Quaternion.identity);
            return;
        }
        //7%
        random = Random.Range(1, 20);
        if (random == 2)
        {
            Instantiate(powerupInvincible, transform.position, Quaternion.identity);
            return;
        }
        //25%
        random = Random.Range(1, 10);
        if (random == 3)
        {
            Instantiate(powerUpShoot, transform.position, Quaternion.identity);
            return;
        }
        //10%
        random = Random.Range(1, 40);
        if (random == 2)
        {
            Instantiate(powerupHealth, transform.position, Quaternion.identity);
            return;
        }
    }
    /// <summary>
    /// Used by child components to determine the bounds 
    /// </summary>
    public enum Bounds
    {
        MinX,
        MaxX,
        MinY,
        MaxY
    }



}
