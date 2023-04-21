using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //elapse time since last enemy spawn
    private float elapsedTime;
    
    //time to respawn enemy
    [SerializeField]
    private float respawnTime;
    
    //time before enemy speed up
    [SerializeField]
    private float speedUpInterval;
    
    //amount to reduce the respawn time
    [SerializeField]
    private float respawnTimeReduction;

    //elapsed time since last speed up
    private float speedUpIntervalElapsed;

    //Boundaries where enemies can spawn
    private float boundsLowX = -9f;
    private float boundsHighX = 9f;
    private float boundsLowY = -2f;
    private float boundsHighY = 4.2f;

    //Enemy game object types
    public GameObject EnemyRand;

    public GameObject EnemyLine;

    public GameObject EnemyFollow;

    public GameObject EnemyFast;

    //Player game object
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //player game object
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //if the player is not destored and there are less than 20 enemies
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 20 && player != null)
        {
            //increase the elapsed time with the delta time
            elapsedTime += Time.deltaTime;

            //if the elapsed time is greater than the respawn time
            if (elapsedTime > respawnTime)
            {
                //reset elapsed time
                elapsedTime = 0;
                //get a random location in X and Y
                float x = Random.Range(boundsLowX, boundsHighX);
                float y = Random.Range(boundsLowY, boundsHighY);
                
                //Enemies spawn depending on the random generated enemy
                int rngRand = Random.Range(1, 21);

                if (rngRand == 3)
                {
                    //spawn enemy random
                    Instantiate(EnemyFast, new Vector2(x, y), Quaternion.identity);
                    return;
                }
                rngRand = Random.Range(1, 11);
                if (rngRand == 5)
                {
                    //spawn enemy random
                    Instantiate(EnemyRand, new Vector2(x, y), Quaternion.identity);
                    return;
                }
               
                rngRand = Random.Range(1, 6);
                if(rngRand == 3)
                    //spawn enemy follow
                    Instantiate(EnemyFollow, new Vector2(x, y), Quaternion.identity);
                else
                    //spawn enemy line
                    Instantiate(EnemyLine, new Vector2(x, y), Quaternion.identity);
            }
        }
        //if the speedUpIntervalElapsed is greater than speedUpInterval
        if(speedUpIntervalElapsed > speedUpInterval)
        {
            //reduce the respawn time
            respawnTime *= respawnTimeReduction;
            //reset speedUpIntervalElapsed to 0
            speedUpIntervalElapsed = 0;
        }

        speedUpIntervalElapsed += Time.deltaTime;
    }
}
