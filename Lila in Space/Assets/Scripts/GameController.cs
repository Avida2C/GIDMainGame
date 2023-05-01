using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Light2D globalLight;

    [SerializeField]
    private GameObject gameOver;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject scoreText;


    // Start is called before the first frame update
    void Start()
    {
        //the gameOver menu is disabled
        gameOver.SetActive(false);
        
        globalLight.gameObject.SetActive(false);
        Invoke("enableLight", 0.3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void enableLight()
    {
        globalLight.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {

    }

    /// <summary>
    /// On player death, the enemies and powerups are destroyed
    /// the gameOver menu is enabled, the panel is disabled and the final score is visible on the gameOver Menu
    /// </summary>
    /// <param name="score"></param>
    public void ShowGameOver(string score)
    {
        //to check if GameObjects with the tag "Enemy" are active
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //to check if GameObjects with the tag "PowerUp" are active
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp");
        
        //Enemies are destroyed
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        
        //Powerups are destroyed
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }

        //gameOver Menu enabled
        gameOver.SetActive(true);
        //panel is disabled
        panel.SetActive(false);
        //the score is visible on the gameOver menu
        scoreText.GetComponent<TMPro.TMP_Text>().text = score;
    }
}
