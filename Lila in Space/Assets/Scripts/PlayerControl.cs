using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Player Movement
    [SerializeField] private float maxSpeed = 7;

    private Vector2 targetVelocity;
    private Rigidbody2D player;

    private Vector2 move;
    
    //Projectile Settings
    [Header("Projectile References")]
    [SerializeField]
    private Transform projectileSource;

    [SerializeField]
    private GameObject projectile;

    private void Awake()
    {
        //Player Movement
        player = GetComponent<Rigidbody2D>();
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
        player.position = player.position + deltaPosition * move.magnitude;
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    
}
