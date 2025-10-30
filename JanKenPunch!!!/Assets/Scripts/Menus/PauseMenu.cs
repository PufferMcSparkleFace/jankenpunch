using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject characterSelectScreen;
    public AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindFirstObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !characterSelectScreen.activeSelf)
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        audioManager.StopPlaying("Fight (Unlooped)");
        audioManager.StopPlaying("Fight (Looped)");
        audioManager.StopAllCoroutines();
        audioManager.Play("Menu (Unlooped)");
        audioManager.StartCoroutine("PlayMenuTheme");
        SceneManager.LoadScene("Start Menu");
    }

    public void Quit()
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        Debug.Log("Quit");
        Application.Quit();
    }
}
