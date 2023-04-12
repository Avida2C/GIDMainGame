using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    public float enemyVelocity = 2f;

    [SerializeField]
    public float enemyUpgradeTime;

    [SerializeField]
    public int maximumHealth;

    public Health health;

    public float boundsLowX = -8.8f;
    public float boundsHighX = 8.8f;
    public float boundsLowY = -2.0f;
    public float boundsHighY = 4.8f;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        health.MaximumHealth = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Projectile" && this.health != null)
        {
            this.health.Decrement();
            if (this.health.IsDead())
                Destroy(gameObject);

        }
    }


    public enum Bounds
    {
        MinX,
        MaxX,
        MinY,
        MaxY
    }
}
