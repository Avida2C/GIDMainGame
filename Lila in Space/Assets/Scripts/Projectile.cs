using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField]
    private float speed = 10;
    
    //Rigibody Component
    private Rigidbody2D _rigidbody;
    

    [SerializeField] private float lifetime = 2;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.gameObject.tag.Contains("Projectile") && !collision.collider.gameObject.tag.Contains("Player"))
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.gameObject.tag.Contains("Projectile") && !collider.gameObject.tag.Contains("Player"))
            Destroy(gameObject);
    }

    public void Shoot()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(transform.up * speed, ForceMode2D.Impulse);
        Destroy(gameObject, lifetime);
    }
}
