using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerupBase : MonoBehaviour
{

  

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
            
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
}
