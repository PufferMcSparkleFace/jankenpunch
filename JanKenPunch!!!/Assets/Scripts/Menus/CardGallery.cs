using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CardGallery : MonoBehaviour
{
    public GameObject characterCard, nonBasicCard, blurb;

    private void Update()
    {
        if(nonBasicCard.activeInHierarchy != true && blurb.activeInHierarchy != true)
        {
            characterCard.SetActive(true);
        }
        else
        {
            characterCard.SetActive(false);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Card Gallery");
    }

}
