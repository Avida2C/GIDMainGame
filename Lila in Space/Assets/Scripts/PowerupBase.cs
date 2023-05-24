using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerupBase : MonoBehaviour
{
    [SerializeField]
    private float pickupDestroy = 10f;

    [SerializeField]
    private float speed = 5f;

    private PlayerControl player;

    [SerializeField]
    private float distance = 5f;

    // Start is called before the first frame update
    public void Start()
    {
        Invoke("destroyPowerUp", pickupDestroy);
        //Player GameObject from PlayerControl
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    public void Update()
    {
        float playerDistance = Vector2.Distance(player.transform.position,transform.position); 
        if (playerDistance < distance)
        {
            pickUpItem();
        }
    }


    /// <summary>
    /// If the object with the tag "Player" collides with the powerup, the powerup gameobject is destroyed
    /// </summary>
    /// <param name="collider"></param>
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            Destroy(gameObject);
    }

    private void destroyPowerUp()
    {
        Destroy(gameObject);
    }

    private void pickUpItem()
    {
        
        if (transform != null && player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    }
}
