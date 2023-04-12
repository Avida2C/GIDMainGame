using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : EnemyBase
{
    private Transform player;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform != null && player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyVelocity * Time.deltaTime);
    }

}
