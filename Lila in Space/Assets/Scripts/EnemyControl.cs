using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D enemy;

    [SerializeField]
    private float boundRight = 2.7f;

    [SerializeField]
    private float boundLeft = -3.7f;

    private bool increase = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (increase)
        {
            Vector2 move = new Vector2(0.01f, 0f);
            enemy.position = enemy.position + move;
            if (enemy.position.x >= boundRight)
                increase = false;
        }
        else
        {
            Vector2 move = new Vector2(-0.01f, 0f);
            enemy.position = enemy.position + move;
            if (enemy.position.x <= boundLeft)
                increase = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }
    
}
