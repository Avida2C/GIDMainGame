using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : EnemyBase
{
    //To attach AudioClip
    [SerializeField]
    private AudioClip Shooting;

    //Enemy Projectile
    [SerializeField]
    private GameObject projectile;

    //Shooting time/Projectile spawning time
    [SerializeField]
    public float shootTime = 1f;

    //Time since last shot projectile
    private float timeElapsedShootTime = 0f;

    //time to change direction
    [SerializeField]
    private readonly float directionChangeTime = 3f;
    //last direction change time
    private float latestDirectionChangeTime;
    //movement
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    //elapsed time since last upgrade
    private float elapsedTimeUpgrade;

    //colours for enemy leveling up
    private Color level1 = Color.white;
    private Color level2 = Color.green;
    private Color level3 = Color.blue;
    private Color level4 = Color.yellow;
    private Color level5 = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        //Calling the Start Method in EnemyBase
        base.Start();
        //Calculate the shoot time depending on the speedMultiplier
        shootTime *= (1f - (gameController.speedMultiplier * 0.1f));

        //enemy colour set to level 1
        sprite.color = level1;
    }

    // Update is called once per frame
    void Update()
    {
        //Adds delta time to timeElapsedShootTime
        timeElapsedShootTime += Time.deltaTime;

        //if timeElapsedShootTime is greater than shootTime
        if (timeElapsedShootTime > shootTime)
        {
            //Shoot Projectile
            this.Shoot();
            //Reset timeElapsedShootTime
            timeElapsedShootTime = 0;


        }

        //check if enemies reached one of the boundaries
        if (transform.position.x >= boundsHighX)
        {
            calcuateNewMovementVectorBounds(Bounds.MaxX);
        }
        else if (transform.position.x <= boundsLowX)
        {
            calcuateNewMovementVectorBounds(Bounds.MinX);
        }
        else if (transform.position.y >= boundsHighY)
        {
            calcuateNewMovementVectorBounds(Bounds.MaxY);
        }
        else if (transform.position.y <= boundsLowY)
        {
            calcuateNewMovementVectorBounds(Bounds.MinY);
        }
        //if time minus latestDirectionChangeTime is greater than directionChangeTime
        else if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            //set latestDirectionChangeTime to current time
            latestDirectionChangeTime = Time.time;
            //calculate new movement
            calcuateNewMovementVector();
        }
        //Change position and movement
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));


    }

    void Shoot()
    {
        //Play the audioclip found in the "Shooting" AudioSource
        audioProperties.PlayOneShot(Shooting);

        //Projectile spawn
        EnemyProjectile projectile4 = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();

        projectile4.Shoot(Vector2.down);
    }


    private void FixedUpdate()
    {
        //add deltaTime to elapsedTimeUpgrade
        elapsedTimeUpgrade += Time.deltaTime;

        //if elapsedTimeUpgrade is greater or equal to enemyUpgradeTime and enemy currentHealth is less than maximumHealth
        if (elapsedTimeUpgrade >= enemyUpgradeTime && this.health.currentHealth < this.maximumHealth)
        {
            //reset elapsedTimeUpgrade
            elapsedTimeUpgrade = 0;
            //Add +1
            this.health.IncrementEnemy();
            //Change colour
            this.SetColor();
        }
    }


    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(boundsLowX, boundsHighX), Random.Range(boundsLowY, boundsHighY)).normalized;
        movementPerSecond = movementDirection * enemyVelocity;
    }

    /// <summary>
    /// if any of the 4 boundaries is hit, calculate new movement vector in the opposite direction
    /// </summary>
    /// <param name="bounds"></param>
    void calcuateNewMovementVectorBounds(Bounds bounds)
    {
        switch (bounds)
        {
            case Bounds.MinX:
                {
                    movementDirection = new Vector2(boundsHighX, Random.Range(boundsLowY, boundsHighY)).normalized;
                    break;
                }
            case Bounds.MaxX:
                {
                    movementDirection = new Vector2(boundsLowX, Random.Range(boundsLowY, boundsHighY)).normalized;
                    break;
                }
            case Bounds.MinY:
                {
                    movementDirection = new Vector2(Random.Range(boundsLowX, boundsHighX), boundsHighY).normalized;
                    break;
                }
            case Bounds.MaxY:
                {
                    movementDirection = new Vector2(Random.Range(boundsLowX, boundsHighX), boundsLowY).normalized;
                    break;
                }

        }
        //Calculate movement speed
        movementPerSecond = movementDirection * enemyVelocity;
    }

    /// <summary>
    /// Set the enemy sprite colour depending on the current health / level
    /// </summary>
    private void SetColor()
    {
        try
        {
            switch (this.health.currentHealth)
            {
                case 1:
                    sprite.color = level1;
                    break;
                case 2:
                    sprite.color = level2;
                    break;
                case 3:
                    sprite.color = level3;
                    break;
                case 4:
                    sprite.color = level4;
                    break;
                case 5:
                    sprite.color = level5;
                    break;
            }
        }
        catch (System.Exception)
        {

        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //Call method OnTriggerEnter2D from EnemyBase
        base.OnTriggerEnter2D(collision);
        //Set colour
        this.SetColor();
    }
}

