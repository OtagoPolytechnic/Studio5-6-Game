using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    private void Start()
    {
        SFXManager.Instance.PlayBackgroundMusic(SFXManager.Instance.TitleScreen);
        Application.targetFrameRate = 60;
    }

    public void Play()
    {
         SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit the game");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void HighscoreButton()
    {
        SceneManager.LoadScene("Highscores");
    }
}


