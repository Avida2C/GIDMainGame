using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOver;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject scoreText;

    [SerializeField]
    private float speedUpInterval;

    private float speedUpIntervalElapsed;

    [HideInInspector]
    public float speedMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (speedUpIntervalElapsed > speedUpInterval)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                EnemyBase enemyComponent = enemy.GetComponent<EnemyBase>();
                enemyComponent.enemyVelocity += 1f;
                enemyComponent = enemy.GetComponent<EnemyLine>();
                if (enemyComponent != null)
                {
                    (enemyComponent as EnemyLine).shootTime *= (1f - (speedMultiplier * 0.1f));
                }
            }
            speedMultiplier += 1f;
            speedUpIntervalElapsed = 0;
        }
        else
        {
            speedUpIntervalElapsed += Time.deltaTime;
        }
    }

    public void ShowGameOver(string score)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }
        gameOver.SetActive(true);
        panel.SetActive(false);
        scoreText.GetComponent<TMPro.TMP_Text>().text = score;
    }
}
