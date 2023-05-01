using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLine : EnemyBase
{
    //To attach AudioClip
    [SerializeField]
    private AudioClip Shooting;

    //Emeny Line Movement
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    //Enemy Projectile
    [SerializeField]
    private GameObject projectile;

    //Shooting time/Projectile spawning time
    [SerializeField]
    public float shootTime = 1f;

    //Time since last shot projectile
    private float timeElapsedShootTime = 0f;

    // Start is called before the first frame update
    new void Start()
    {
        //Calling the Start Method in EnemyBase
        base.Start();

        //Start movement
        if (transform.position.x > boundsHighX)
            calcuateNewMovementVector(Bounds.MaxX);
        else
            calcuateNewMovementVector(Bounds.MinX);
    }

    // Update is called once per frame
    void Update()
    {
        if (!newSpawn)
        {
            //if the enemy reaches maximum X boundary
            if (transform.position.x >= boundsHighX)
            {
                //calculate movement from right to left
                calcuateNewMovementVector(Bounds.MaxX);
            }
            //if the enemy reached the minimum X boundary
            else if (transform.position.x <= boundsLowX)
            {
                //calculate movement from left to right
                calcuateNewMovementVector(Bounds.MinX);
            }
        }
        
        //enemy movement
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
        
        //Adds delta time to timeElapsedShootTime
        timeElapsedShootTime += Time.deltaTime;

        //if timeElapsedShootTime is greater than shootTime
        if(timeElapsedShootTime > shootTime)
        {
            //Shoot Projectile
            this.Shoot();
            //Reset timeElapsedShootTime
            timeElapsedShootTime = 0;
        }
    }

    void calcuateNewMovementVector(Bounds bounds)
    {
        //if enemy reaches maximum X, set movement direction to left
        if(bounds == Bounds.MaxX)
        {
            movementDirection = new Vector2(-1, -0.05f);
        }
        //if enemy reaches minimum X, set movement direction to right
        else
        {
            movementDirection = new Vector2(1, -0.05f);
        }
        //Calculate movement speed
        movementPerSecond = movementDirection * enemyVelocity;
    }

    void Shoot()
    {
        //Play the audioclip found in the "Shooting" AudioSource
        audioProperties.PlayOneShot(Shooting);

        //Projectile spawn
        EnemyProjectile projectile4 = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        
        projectile4.Shoot(Vector2.down);
    }
}
