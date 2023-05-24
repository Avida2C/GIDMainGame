using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHealth : PowerupBase
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player collides with GameObject "PowerUpHalth"
        if(collision.gameObject.tag == "Player")
        {
            //Calls the OnHealthPowerUp method in the PlayerControl Component/script
            collision.gameObject.GetComponent<PlayerControl>().OnHealthPowerUp();
            base.OnTriggerEnter2D(collision);
        }
    }
}
