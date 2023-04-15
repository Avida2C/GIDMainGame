using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Unity Setup")] public ParticleSystem DeathParticleSystem;

    [SerializeField]
    public float enemyVelocity = 1f;

    [SerializeField]
    public float enemyUpgradeTime;

    [SerializeField]
    public int maximumHealth;

    [SerializeField]
    private GameObject powerupInvincible;

    [SerializeField]
    private GameObject powerUpShoot;

    [SerializeField]
    private GameObject powerupNuke;

    [SerializeField]
    private GameObject powerupHealth;

    [HideInInspector]
    public AudioSource audioProperties; 

    [SerializeField]
    private AudioClip Dead;

    [HideInInspector]
    public GameController gameController;

    [HideInInspector]
    public Health health;

    public float boundsLowX = -8.8f;
    public float boundsHighX = 8.8f;
    public float boundsLowY = -2.0f;
    public float boundsHighY = 4.0f;

    private BoxCollider2D colliderOnSpawn;

    [HideInInspector]
    public SpriteRenderer sprite;

    [HideInInspector]
    public PlayerControl player;

    // Start is called before the first frame update
    public void Start()
    {
        audioProperties = GameObject.FindWithTag("audioSource").GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        health.MaximumHealth = 1;
        enemyVelocity += gameController.speedMultiplier;
        
        colliderOnSpawn = GetComponent<BoxCollider2D>();
        colliderOnSpawn.enabled = false;
        Invoke("EnableHitBox", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Projectile" && this.health != null)
        {
            this.health.Decrement();
            if (this.health.IsDead())
            {
                audioProperties.PlayOneShot(Dead);
                this.Drops();
                player.AddKill();
                Instantiate(DeathParticleSystem, transform.position, Quaternion.identity);
                Destroy(gameObject);
                
            }

        }
        else if(collider.tag == "Player")
            {
            Destroy(gameObject);
        }
    }

    public void Drops()
    {
        int random = Random.Range(1, 20);
        if (random == 4)
        {
            Instantiate(powerupNuke, transform.position, Quaternion.identity);
            return;
        }
        random = Random.Range(1, 15);
        if (random == 2)
        {
            Instantiate(powerupInvincible, transform.position, Quaternion.identity);
            return;
        }
        random = Random.Range(1, 5);
        if (random == 3)
        {
            Instantiate(powerUpShoot, transform.position, Quaternion.identity);
            return;
        }
        random = Random.Range(1, 11);
        if (random == 2)
        {
            Instantiate(powerupHealth, transform.position, Quaternion.identity);
            return;
        }
    }

    public enum Bounds
    {
        MinX,
        MaxX,
        MinY,
        MaxY
    }

    public void EnableHitBox()
    {
        colliderOnSpawn.enabled = true;
    }


}
