using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CardGallery : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("Card Gallery");
    }
}
