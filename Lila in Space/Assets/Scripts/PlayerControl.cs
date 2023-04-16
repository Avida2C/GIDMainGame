using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    //Player Movement 
    [SerializeField] 
    private float maxSpeed = 7;
    private Vector2 move;
    private Vector2 targetVelocity;

    //Rigidbody component
    private Rigidbody2D body;
    //Health component (script)
    private Health health;
    
        //Projectile Settings
    [Header("Projectile References")]
    [SerializeField]
    private Transform projectileSource;

    [SerializeField]
    private GameObject projectile;

    //player invulnerable 
    private bool invulnerable;
    [SerializeField]
    private float invulnerableTime = 2f;

    //Powerup score if health is full
    [SerializeField]
    private int healthPowerupPoints = 20;

    //Shoot powerUp list
    private List<PowerupBase> powerups;

    //score per enemy kill
    [SerializeField]
    private int killPoints = 5;

    //player sprite
    private SpriteRenderer sprite;

    //Player Invincible (from PowerUp)
    private bool invincible;

    //total score
    [HideInInspector]
    public int points = 0;

    //Score textbox
    private TMPro.TMP_Text scoreAmount;

    //AudioSource will allow AudioClips to play
    private AudioSource audioProperties;
    //To attach AudioClip
    [SerializeField]
    private AudioClip Hit;

    [SerializeField]
    private AudioClip Shoot;

    [SerializeField]
    private AudioClip Dead;

    [SerializeField]
    private AudioClip PickUp;


    private void Awake()
    {
        //Get the audioSource Component with the tag "audioSource" 
        audioProperties = GameObject.FindWithTag("audioSource").GetComponent<AudioSource>();

        //get rigidbody
        body = GetComponent<Rigidbody2D>();
        //get health script
        health = GetComponent<Health>();
        //get sprite 
        sprite = GetComponent<SpriteRenderer>();
        //get list of powerUp for shooting
        powerups = new List<PowerupBase>();
        //get score text component
        scoreAmount = GameObject.FindGameObjectWithTag("Score").GetComponent<TMPro.TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement and shooting controls
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        targetVelocity = Vector2.zero;
        
        ComputeVelocity();
        
        if (Input.GetButtonDown("Jump"))
        {
            //Projectile 
            fire();
        }
        //if the player is invincible
        if (this.invincible)
        {
            //set rainbow effect on Player sprite
            sprite.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 2, 1), 1, 1)));
        }

        //show score amount
        scoreAmount.text = points.ToString();
    }

    /// <summary>
    /// Shooting of Player projectile depending on the amount of powerup collected by the player
    /// </summary>
    void fire()
    {
        //create a bullet
        audioProperties.PlayOneShot(Shoot);
        int shootingPowerups = powerups.FindAll(p => p is PowerupShoot).Count;

        if (shootingPowerups == 1)
        {
            var projectile1 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 30)).GetComponent<Projectile>();
            var projectile2 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -30)).GetComponent<Projectile>();
            projectile1.Shoot();
            projectile2.Shoot();
        }
        else if (shootingPowerups == 2)
        {
            var projectile1 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 30)).GetComponent<Projectile>();
            var projectile2 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Projectile>();
            var projectile3 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -30)).GetComponent<Projectile>();
            projectile1.Shoot();
            projectile2.Shoot();
            projectile3.Shoot();

        }
        else if (shootingPowerups == 3)
        {
            var projectile1 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 30)).GetComponent<Projectile>();
            var projectile2 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -30)).GetComponent<Projectile>();
            var projectile3 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Projectile>();
            var projectile4 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Projectile>();
            var projectile5 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -90)).GetComponent<Projectile>();
            projectile1.Shoot();
            projectile2.Shoot();
            projectile3.Shoot();
            projectile4.Shoot();
            projectile5.Shoot();
        }
        else if (shootingPowerups == 4)
        {
            var projectile1 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 30)).GetComponent<Projectile>();
            var projectile2 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -30)).GetComponent<Projectile>();
            var projectile3 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Projectile>();
            var projectile4 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Projectile>();
            var projectile5 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -90)).GetComponent<Projectile>();
            var projectile6 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -150)).GetComponent<Projectile>();
            var projectile7 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 150)).GetComponent<Projectile>();
            projectile1.Shoot();
            projectile2.Shoot();
            projectile3.Shoot();
            projectile4.Shoot();
            projectile5.Shoot();
            projectile6.Shoot();
            projectile7.Shoot();
        }

        else if (shootingPowerups >= 5)
        {
            var projectile1 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 30)).GetComponent<Projectile>();
            var projectile2 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -30)).GetComponent<Projectile>();
            var projectile3 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Projectile>();
            var projectile4 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Projectile>();
            var projectile5 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -90)).GetComponent<Projectile>();
            var projectile6 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, -150)).GetComponent<Projectile>();
            var projectile7 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 150)).GetComponent<Projectile>();
            var projectile8 = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, 180)).GetComponent<Projectile>();
            projectile1.Shoot();
            projectile2.Shoot();
            projectile3.Shoot();
            projectile4.Shoot();
            projectile5.Shoot();
            projectile6.Shoot();
            projectile7.Shoot();
            projectile8.Shoot();
        }
        //if there are no powerup collected, shoot this
        else
        {
            var projectileShot = Instantiate(projectile, gameObject.transform.position, projectileSource.rotation).GetComponent<Projectile>();
            projectileShot.Shoot();
        }
    }

    private void OnBecameInvisible()
    {
        //player can move from left to right from each side of the screen
        ScreenWrap();
    }

    protected void ComputeVelocity()
    {
        //Player Movement
        targetVelocity = move * maxSpeed;
    }

    private void FixedUpdate()
    {
        //Player Movement
        var deltaPosition = targetVelocity * Time.deltaTime;
        body.position = body.position + deltaPosition * move.magnitude;
    }
    
    /// <summary>
    /// The player moves from one side of the screen to the other (X axis only)
    /// </summary>
    void ScreenWrap()
    {
        var cam = Camera.main;
        if (cam != null)
        {
            var viewportPosition = cam.WorldToViewportPoint(transform.position);
            var newPosition = transform.position;
            if ((viewportPosition.x > 1 || viewportPosition.x < 0))
            {
                newPosition.x = -newPosition.x;
            }

            if ((viewportPosition.y > 1 || viewportPosition.y < 0))
            {
                newPosition.y = -newPosition.y;
            }
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if player is not invulnerable or invincible and collides with an enemy or enemy projectile 
        if ((collider.tag == "Enemy" || collider.tag == "EnemyProjectile") && !invulnerable && !invincible)
        {
            //get and hide one of the health sprites
            GameObject.Find("Health_" + this.health.currentHealth).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            //decrease player health -1
            health.Decrement();
            //player is set to invulnerable
            SetInvulnerable();
            //reset player to vulnerable after invulnerableTime elapsed
            Invoke("SetVulnerable", invulnerableTime);
            //call flasher
            StartCoroutine("Flasher");
            //remove all shooting powerups the player collected
            powerups.Clear();

            //check if player is dead
            if (health.IsDead())
            {
                //Play the audioclip found in the "Dead" AudioSource
                audioProperties.PlayOneShot(Dead);
                
                SaveHighScores();
                //show game over screen
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ShowGameOver(points.ToString());
                //destroy the player gameobject
                Destroy(gameObject);
            }
            else
            {
                //Play the audioclip found in the "Hit" AudioSource
                audioProperties.PlayOneShot(Hit);
            }
        }
        //if player collects powerup
        else if (collider.tag == "PowerUp")
        {
            //play audioclip PickUp
            audioProperties.PlayOneShot(PickUp);

            //get the powerup component
            PowerupBase powerup = collider.gameObject.GetComponent<PowerupBase>();
            //if powerup is of type PowerupShoot
            if (powerup is PowerupShoot)
            {
                //if player has 5 or more powerups
                if (this.powerups.Count >= 5)
                {
                    //player get score instead of powerup
                    points += healthPowerupPoints;
                }
                else
                {
                    //upgrade shooting method from the powerup list
                    powerups.Add(powerup);
                }
            }

        }
    }
    /// <summary>
    /// on invulenrable, the player's sprite changes colours between white and pink every 0.1 seconds
    /// to simulate blinking
    /// </summary>
    /// <returns></returns>
    IEnumerator Flasher()
    {
        int loop = ((int)invulnerableTime) * 5;
        for (int i = 0; i < loop; i++)
        {
            sprite.color = new Color(255, 0, 101, 255);
            yield return new WaitForSeconds(.1f);
            sprite.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(.1f);
        }
    }


    public void SetInvulnerable()
    {
        this.invulnerable = true;
    }

    public void SetVulnerable()
    {
        this.invulnerable = false;
    }

    public void SetInvincible()
    {
        this.invincible = true;
    }

    public void SetVincible()
    {
        this.invincible = false;
        //Sprite colour is set to white
        sprite.material.SetColor("_Color", Color.white);
    }

    /// <summary>
    /// Player is set to invincible for a limited amount of time
    /// </summary>
    /// <param name="time"></param>
    public void SetInvincible(float time)
    {
        this.SetInvincible();
        Invoke("SetVincible", time);

    }
    /// <summary>
    /// gain health on pick up of powerupHealth, if player is on full health, the player gets score increase
    /// </summary>
    public void OnHealthPowerUp()
    {
        if (this.health.currentHealth < 3)
        {
            this.health.Increment();
            GameObject.Find("Health_" + this.health.currentHealth).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        }
        else
            points += healthPowerupPoints;
    }

    /// <summary>
    /// Add score on enemy kill
    /// </summary>
    public void AddKill()
    {
        points += killPoints;
    }
    /// <summary>
    /// Save highscore to PlayerPrefs if the score is higher than the previous Highscore
    /// </summary>
    private void SaveHighScores()
    {
        if (points > PlayerPrefs.GetInt("HighScores"))
        {
            PlayerPrefs.SetInt("HighScores", points);
        }
    }

}
