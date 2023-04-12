using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    //Rigibody Component
    private Rigidbody2D _rigidbody;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.gameObject.tag.Contains("Enemy") && !collider.gameObject.tag.Contains("Projectile"))
            Destroy(gameObject);
    }

    public void Shoot(Vector2 direction)
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);

        Destroy(gameObject, lifetime);
    }
}
