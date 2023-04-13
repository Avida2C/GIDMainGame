using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupNuke : PowerupBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl player = GameObject.Find("Player").GetComponent<PlayerControl>();
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
                player.AddKill();
            }
            base.OnTriggerEnter2D(collision);
        }
    }
}
