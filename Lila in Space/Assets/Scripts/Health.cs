using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //Maximum Health
    public int MaximumHealth = 1;
    //The current health of gameObjects
    [HideInInspector]
    public int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        //The current health of gameObjects is set to the maximum health
        this.currentHealth = this.MaximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// To increase the HP of the player
    /// </summary>
    public void Increment()
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth + 1, 0, this.MaximumHealth);
    }

    /// <summary>
    /// To increase the HP of the enemy
    /// </summary>
    public void IncrementEnemy()
    {
        if(this.currentHealth == this.MaximumHealth) 
        {
            this.MaximumHealth++;
        }
        this.currentHealth = Mathf.Clamp(this.currentHealth + 1, 0, this.MaximumHealth);
    }

    /// <summary>
    /// Will decrese the HP of the gameobjects
    /// </summary>
    public void Decrement()
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth - 1, 0, this.MaximumHealth);
    }

    /// <summary>
    /// Checks if the gameObject should be dead
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return this.currentHealth <= 0;
    }

   
}
