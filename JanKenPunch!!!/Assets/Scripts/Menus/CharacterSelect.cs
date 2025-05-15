using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void SelectZyla()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Zyla");
    }

    public void SelectTaibo()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Taibo");
    }

    public void SelectRynox()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Rynox");
    }
}
