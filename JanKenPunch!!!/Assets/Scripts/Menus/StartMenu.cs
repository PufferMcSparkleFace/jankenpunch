using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindFirstObjectByType<AudioManager>();
    }

    public void CharacterSelect()
    {
        SceneManager.LoadScene("Game");
    }

    public void CardGallery()
    {
        audioManager.Play("Card Gallery (Unlooped)");
        audioManager.StopAllCoroutines();
        audioManager.StartCoroutine("PlayCardGalleryTheme");
        audioManager.StopPlaying("Menu (Unlooped)");
        audioManager.StopPlaying("Menu (Looped)");
        SceneManager.LoadScene("Card Gallery");
    }

    public void Tutorial()
    {
        audioManager.Play("Tutorial (Unlooped)");
        audioManager.StopAllCoroutines();
        audioManager.StartCoroutine("PlayTutorialTheme");
        audioManager.StopPlaying("Menu (Unlooped)");
        audioManager.StopPlaying("Menu (Looped)");
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Back()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            audioManager.StopPlaying("Tutorial (Unlooped)");
            audioManager.StopPlaying("Tutorial (Looped)");
        }
        if(SceneManager.GetActiveScene().name == "Card Gallery")
        {
            audioManager.StopPlaying("Card Gallery (Unlooped)");
            audioManager.StopPlaying("Card Gallery (Looped)");
        }
        audioManager.StopAllCoroutines();
        audioManager.Play("Menu (Unlooped)");
        audioManager.StartCoroutine("PlayMenuTheme");
        SceneManager.LoadScene("Start Menu");
    }
}
