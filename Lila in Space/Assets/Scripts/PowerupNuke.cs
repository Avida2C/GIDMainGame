using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupNuke : PowerupBase
{
    [Header("Unity Setup")] public ParticleSystem DeathParticleSystem;

    //AudioSource to enable audioclips
    private AudioSource audioProperties;

    //To attach AudioClip
    [SerializeField]
    private AudioClip Nuke;

    // Start is called before the first frame update
    void Start()
    {
        //Get the audioSource Component with the tag "audioSource" 
        audioProperties = GameObject.FindWithTag("audioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Play the audioclip found in the "Nuke" AudioSource
            audioProperties.PlayOneShot(Nuke);
            //Get the player component with the tag "Player"
            PlayerControl player = GameObject.Find("Player").GetComponent<PlayerControl>();
            //Get all spawned gameobjects with the tag "Enemy"
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach (var enemy in enemies)
            {
                //Get the EnemyBase component for each enemy and call the method Drops
                enemy.GetComponent<EnemyBase>().Drops(); 
                //Destroy the enemy Object
                Destroy(enemy);
                //Call AddKill method on the player component
                player.AddKill();
            }
            
            base.OnTriggerEnter2D(collision);

            //Start Particle System 
            Instantiate(DeathParticleSystem, transform.position, Quaternion.identity);
        }
    }
}
