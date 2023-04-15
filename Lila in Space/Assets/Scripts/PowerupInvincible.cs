using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvincible : PowerupBase
{
    //Variable to input the amount of time the powerup is active
    [SerializeField]
    private float powerupTime;

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
        //if gameobject with the tag "Player" collides with this gameObject
        if (collision.gameObject.tag == "Player")
        {
            //Call the method SetInvincible from PlayerControl component/script
            PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
            player.SetInvincible(powerupTime);
            base.OnTriggerEnter2D(collision);
        }
        
    }

    
}
