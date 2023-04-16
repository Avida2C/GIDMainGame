using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    //speed of the projectile
    [SerializeField]
    private float speed = 10;

    //Rigibody Component
    private Rigidbody2D _rigidbody;

    //lifetime of the projectile
    [SerializeField] 
    private float lifetime = 2;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// If the tag is not "Enemy, Projectile or Powerup" the projectile is destroyed
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    { 
        if(!collider.gameObject.tag.Contains("Enemy") && !collider.gameObject.tag.Contains("Projectile") && !collider.gameObject.tag.Contains("PowerUp"))
            Destroy(gameObject);
    }

    /// <summary>
    /// Adds force to the projectile
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot(Vector2 direction)
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
        //Projectile is destroyed after lifetime has elapsed 
        Destroy(gameObject, lifetime);
    }
}
