using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TitleScreen : MonoBehaviour
{

    public TMP_Text clickAnywhere;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayTrailer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Start Menu");
        }
    }

    IEnumerator PlayTrailer()
    {
        yield return new WaitForSecondsRealtime(10);
        Debug.Log("Play Trailer");
        //disable title screen image, adjust the position of the click anywhere, play trailer
    }
}
