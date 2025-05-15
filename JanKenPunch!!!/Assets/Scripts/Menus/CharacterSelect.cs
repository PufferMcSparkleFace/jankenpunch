using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void SelectZyla()
    {
        Debug.Log("Zyla");
        this.gameObject.SetActive(false);
    }

    public void SelectTaibo()
    {
        Debug.Log("Taibo");
        this.gameObject.SetActive(false);
    }

    public void SelectRynox()
    {
        Debug.Log("Rynox");
        this.gameObject.SetActive(false);
    }
}
