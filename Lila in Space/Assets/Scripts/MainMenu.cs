using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public AudioSource audioSource;

    public void PlayGame()
    {
        StartCoroutine(Delay());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AudioSource()
    {

        audioSource.Play();
    }

    IEnumerator Delay()
    {
     
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    IEnumerator ChangeDelay()
    {

        yield return new WaitForSeconds(0.5f);
        

    }

}
