using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //will determine the speed of the projectile
    [SerializeField]
    private float speed = 10;
    
    //Rigibody Component
    private Rigidbody2D _rigidbody;
    
    //will determine the lifetime of the projectile
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
        //Projectile is destoyed if it collides with an object which is not a powerup or the player or an other projectile
        if (!collision.collider.gameObject.tag.Contains("Projectile") && !collision.collider.gameObject.tag.Contains("Player") && !collision.collider.gameObject.tag.Contains("Powerup"))
            Destroy(gameObject);
    }



    public void Shoot()
    {
        //if the rigidbody is not set, get the rigidbody2D component
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        //Sets the force of the projectile
        _rigidbody.AddForce(transform.up * speed, ForceMode2D.Impulse);
        
        //The projectile is destoryed after the lifetime has elapsed
        Destroy(gameObject, lifetime);
    }
}
