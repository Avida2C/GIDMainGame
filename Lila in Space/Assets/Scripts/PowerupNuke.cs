using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupNuke : PowerupBase
{
    [Header("Unity Setup")] public ParticleSystem DeathParticleSystem;

    private AudioSource audioProperties;

    [SerializeField]
    private AudioClip Nuke;

    // Start is called before the first frame update
    void Start()
    {
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
            audioProperties.PlayOneShot(Nuke);
            PlayerControl player = GameObject.Find("Player").GetComponent<PlayerControl>();
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyBase>().Drops(); 
                Destroy(enemy);
                player.AddKill();
            }
            base.OnTriggerEnter2D(collision);

            Instantiate(DeathParticleSystem, transform.position, Quaternion.identity);
        }
    }
}
