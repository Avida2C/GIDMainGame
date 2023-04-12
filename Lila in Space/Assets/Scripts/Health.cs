using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //Health Parameters
    public int MaximumHealth = 1;

    [HideInInspector]
    public int currentHealth;

    [SerializeField]
    public GameObject healthUI;


    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = this.MaximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Increment the HP of the entity.
    /// </summary>
    public void Increment()
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth + 1, 0, this.MaximumHealth);
    }

    /// <summary>
    /// Increment the HP of the entity.
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
    /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
    /// current HP reaches 0.
    /// </summary>
    public void Decrement()
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth - 1, 0, this.MaximumHealth);
        //if (this.currentHealth == 0)
        //{
        //    var ev = Schedule<HealthIsZero>();
        //    ev.health = this;
        //}
    }

    /// <summary>
    /// Checks if the gameObject should be dead
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return this.currentHealth <= 0;
    }

    public void SetHealthBar()
    {
        Canvas c = GameObject.Find("Canvas").GetComponent<Canvas>();
        for (int i = 0; i < this.currentHealth; i++)
        {
            if (GameObject.Find("Health_" + (i + 1)) != null)
                Destroy(GameObject.Find("Health_" + (i + 1)));
            int move = 90 * i;
            GameObject obj = Instantiate(healthUI, c.transform);
            obj.name = "Health_" + (i + 1);
            obj.transform.position = new Vector2(obj.transform.position.x + move, obj.transform.position.y);
        }
    }
}
