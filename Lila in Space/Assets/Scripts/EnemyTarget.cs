using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : EnemyBase
{
    // Start is called before the first frame update
    new void Start()
    {
        //Calls the Start method of the parent class - EnemyBase
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player exists, the enemy will follow the player
        if(transform != null && player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyVelocity * Time.deltaTime);

        
    }




}
