using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Added UnityEngine.SceneManagement to utilize the SceneManagement Properties which allow me to change scenes
/// </summary>
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //AudioSource for Audio Effects to play when clicking menu options
    public AudioSource audioSource;

    //TMPRO.TMP_Text to create a variable to store the highest score acheived during gameplay
    private TMPro.TMP_Text HScore;

    /// <summary>
    /// Starts the game scene
    /// </summary>
    public void PlayGame()
    {
        //Starts IEnumerator Delay
        StartCoroutine(Delay());
    }

    /// <summary>
    /// This will close the application/game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// This is will play audioclips such as music and sound effects 
    /// </summary>
    public void AudioSource()
    {

        audioSource.Play();
    }

    /// <summary>
    /// Will delay the game scene from loading by 1second and then loads the 
    /// next scene depending on the build settings
    /// </summary>
    /// <returns></returns>
    IEnumerator Delay()
    {
        //Delay for 1 second
        yield return new WaitForSeconds(1f);
        //Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void Start()
    {
        //Getting the text component with the tag name "Score" of the type TMPro.TMP_Text
        HScore = GameObject.FindGameObjectWithTag("Score").GetComponent<TMPro.TMP_Text>();
        //Stores the highest score found in the Player preferences
        int score = PlayerPrefs.GetInt("HighScores");
        //Converts the score (int) to string and shows it in hsnumber_txt
        HScore.text = score.ToString();
    }

    /// <summary>
    /// Loads the scene "menu"
    /// </summary>
    public void returnMainMenu()
    {
        
        SceneManager.LoadScene("menu");
    }

    /// <summary>
    /// Loads the scene "game"
    /// </summary>
    public void refreshScene()
    {
        SceneManager.LoadScene("game");
    }
}
