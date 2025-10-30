using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardGalleryMain : MonoBehaviour
{
    public void Zyla()
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        SceneManager.LoadScene("Card Gallery Zyla");
    }

    public void Taibo()
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        SceneManager.LoadScene("Card Gallery Taibo");
    }

    public void Rynox()
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        SceneManager.LoadScene("Card Gallery Rynox");
    }
}