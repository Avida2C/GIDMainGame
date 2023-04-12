using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : EnemyBase
{
    [SerializeField]
    private readonly float directionChangeTime = 3f;

    private float latestDirectionChangeTime;
    
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    
    private float elapsedTimeUpgrade;

    private Color level1 = Color.white;
    private Color level2 = Color.green;
    private Color level3 = Color.blue;
    private Color level4 = Color.yellow;
    private Color level5 = Color.red;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        sprite.color = level1;
    }

    // Update is called once per frame
    void Update()
    {        
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
        else if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            //sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        elapsedTimeUpgrade += Time.deltaTime;
        if (elapsedTimeUpgrade >= enemyUpgradeTime && this.health.currentHealth < this.maximumHealth)
        {
            elapsedTimeUpgrade = 0;
            this.health.IncrementEnemy();
            this.SetColor();
        }
    }


    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(boundsLowX, boundsHighX), Random.Range(boundsLowY, boundsHighY)).normalized;
        movementPerSecond = movementDirection * enemyVelocity;
    }

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
        movementPerSecond = movementDirection * enemyVelocity;
    }

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
        base.OnTriggerEnter2D(collision);
        this.SetColor();
    }
}
