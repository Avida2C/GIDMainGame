using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : EnemyBase
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform != null && player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyVelocity * Time.deltaTime);

        
    }




}
