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

    //private Text healthText;

    private SpriteRenderer sprite;

    private bool invulnerable;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        //healthText = GameObject.Find("HealthText").GetComponent<Text>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //healthText.text = health.currentHealth.ToString();
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

    }
    
    void fire()
    {
        //create a bullet
        Instantiate(projectile, projectileSource.position, projectileSource.rotation);
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
        if(collider.tag == "Enemy" && !invulnerable)
        {
            health.Decrement();
           // healthText.text = health.currentHealth.ToString();
            SetInvulnerable();
            Invoke("SetVulnerable", invulnerableTime);
            StartCoroutine("Flasher");
            if (health.IsDead())
            {
                Destroy(gameObject);
                //healthText.text = "YOU DIED";
            }
        }
    }

    IEnumerator Flasher()
    {
        int loop = ((int)invulnerableTime) * 5;
        for (int i = 0; i < loop; i++)
        {
            sprite.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(.1f);
            sprite.color = new Color(255, 0, 101, 255);
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

}
