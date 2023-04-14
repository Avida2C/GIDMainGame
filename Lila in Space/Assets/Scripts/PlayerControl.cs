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

    private Vector2 targetVelocity;
    private Rigidbody2D body;
    private Health health;

    private Vector2 move;
    
    //Projectile Settings
    [Header("Projectile References")]
    [SerializeField]
    private Transform projectileSource;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private float invulnerableTime = 2f;

    [SerializeField]
    private int healthPowerupPoints = 20;

    [SerializeField]
    private int killPoints = 5;

    private SpriteRenderer sprite;

    private bool invulnerable;

    private bool invincible;

    private List<PowerupBase> powerups;

    private TMPro.TMP_Text scoreAmount;

    [HideInInspector]
    public int points = 0;

    //Audio
    private AudioSource audioProperties;

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
        audioProperties = GameObject.FindWithTag("audioSource").GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        sprite = GetComponent<SpriteRenderer>();
        powerups = new List<PowerupBase>();
        scoreAmount = GameObject.FindGameObjectWithTag("Score").GetComponent<TMPro.TMP_Text>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        targetVelocity = Vector2.zero;
        ComputeVelocity();
        if (Input.GetButtonDown("Jump"))
        {
            //Projectile 
            fire();
        }
        if (this.invincible)
        {
            sprite.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 2, 1), 1, 1)));
        }

        scoreAmount.text = points.ToString();

       
    }

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

        else
            {
                var projectileShot = Instantiate(projectile, gameObject.transform.position, projectileSource.rotation).GetComponent<Projectile>();
                projectileShot.Shoot();
            }


        

    }

    private void OnBecameInvisible()
    {
        //Screen Boundary
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
    
    //Screen Boundary
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
        if ((collider.tag == "Enemy" || collider.tag == "EnemyProjectile") && !invulnerable && !invincible)
        {
            GameObject.Find("Health_" + this.health.currentHealth).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            health.Decrement();
            SetInvulnerable();
            Invoke("SetVulnerable", invulnerableTime);
            StartCoroutine("Flasher");
            powerups.Clear();


            if (health.IsDead())
            {
                audioProperties.PlayOneShot(Dead);
                SaveHighScores();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ShowGameOver(points.ToString());
                Destroy(gameObject);
            }
            else
            {
                audioProperties.PlayOneShot(Hit);
            }
        }
        else if (collider.tag == "PowerUp")
        {
            audioProperties.PlayOneShot(PickUp);
            PowerupBase powerup = collider.gameObject.GetComponent<PowerupBase>();
            if (powerup is PowerupShoot)
            {
                if (this.powerups.Count >= 5)
                {
                    points += healthPowerupPoints;
                }
                else
                {
                    powerups.Add(powerup);
                }
            }

        }
    }

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
        sprite.material.SetColor("_Color", Color.white);
    }

    public void SetInvincible(float time)
    {
        this.SetInvincible();
        Invoke("SetVincible", time);

    }

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

    public void AddKill()
    {
        points += killPoints;
    }

    private void SaveHighScores()
    {
        if (points > PlayerPrefs.GetInt("HighScores"))
        {
            PlayerPrefs.SetInt("HighScores", points);
        }
    }

}
