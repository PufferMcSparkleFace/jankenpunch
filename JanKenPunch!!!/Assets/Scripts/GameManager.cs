using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playArea;
    public GameObject lockInButton;
    public GameObject playedCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (playArea.childCount == 0)
            {
                if(playedCard != null)
            {
                playedCard.transform.localScale = new Vector3(2.5f, 3.5f, 0);
                playedCard = null;
            }                 
                lockInButton.SetActive(false);           
            }
            else
            {
                lockInButton.SetActive(true);
                playedCard = playArea.GetChild(0).gameObject;
                playedCard.transform.localScale = new Vector3(5, 7, 0);
            }
        
    }

    public void LockIn()
    {
        if(playedCard.tag == "Basic Cards")
        {
            Debug.Log("Played Basic Card");
        }
        else
        {
            Debug.Log("Played Non-Basic Card");
        }
    }
}
