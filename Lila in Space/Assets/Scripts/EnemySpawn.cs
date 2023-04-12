using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private float elapsedTime;

    [SerializeField]
    private float respawnTime;

    private float boundsLowX = -9f;
    private float boundsHighX = 9f;
    private float boundsLowY = -2f;
    private float boundsHighY = 5f;

    public GameObject EnemyRand;

    public GameObject EnemyLine;

    public GameObject EnemyFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 10)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > respawnTime)
            {
                elapsedTime = 0;
                float x = Random.Range(boundsLowX, boundsHighX);
                float y = Random.Range(boundsLowY, boundsHighY);
                int rngRand = Random.Range(1, 11);
                if(rngRand == 5)
                    Instantiate(EnemyRand, new Vector2(x, y), Quaternion.identity);
                rngRand = Random.Range(1, 6);
                if(rngRand == 3)
                    Instantiate(EnemyFollow, new Vector2(x, y), Quaternion.identity);
                else
                    Instantiate(EnemyLine, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
}
