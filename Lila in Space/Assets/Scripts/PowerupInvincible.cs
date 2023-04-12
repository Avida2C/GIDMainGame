using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvincible : PowerupBase
{
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
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
            player.SetInvincible(powerupTime);
            base.OnTriggerEnter2D(collision);
        }
        
    }

    
}
