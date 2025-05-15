using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void CharacterSelect()
    {
        SceneManager.LoadScene("Game");
    }

    public void CardGallery()
    {
        SceneManager.LoadScene("Card Gallery");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
