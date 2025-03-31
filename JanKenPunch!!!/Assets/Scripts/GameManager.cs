using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform playArea;
    public GameObject lockInButton;
    public GameObject playedCard;
    
    public GameObject playedCardObject;
    public TMP_Text playedCardName;

    public int turnTimer;
    public TMP_Text turnTimerText;

    public Transform hand = null;

    public int p1Health;
    public int p1Energy;
    public int p1PlusFrames;
    public int p2Health;
    public int p2Energy;
    public int p2PlusFrames;
    public int finalCardCost;
    public TMP_Text p1HealthText;
    public TMP_Text p1EnergyText;
    public TMP_Text p1PlusFramesText;
    public TMP_Text p2HealthText;
    public TMP_Text p2EnergyText;
    public TMP_Text p2PlusFramesText;

    // Start is called before the first frame update
    void Start()
    {
        p1HealthText.text = p1Health.ToString();
        p1EnergyText.text = p1Energy.ToString();
        p2HealthText.text = p2Health.ToString();
        p2EnergyText.text = p2Energy.ToString();

        hand = GameObject.FindGameObjectWithTag("Hand").transform;
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
                playedCardObject = playedCard.transform.GetChild(0).gameObject;
                playedCardName = playedCardObject.GetComponent<TMP_Text>();
            }
            if(p1PlusFrames == 0)
        {
            p1PlusFramesText.text = "";
        }
        if (p2PlusFrames == 0)
        {
            p2PlusFramesText.text = "";
        }

    }

    public void LockIn()
    {
        Debug.Log("" + playedCardName.text);

        //finalCardCost = card cost - plus frames
        if(finalCardCost < 0)
        {
            finalCardCost = 0;
        }

        if(playedCard.tag == "Basic Cards")
        {
            if(p1Energy >= p2Energy)
            {
                p1Energy--;
                //replace with p1Energy = p1Energy - finalCardCost;
            }
            playedCard.transform.SetParent(hand);
            playedCard.transform.localScale = new Vector3(2.5f, 3.5f, 0);
            playedCard = null;
            lockInButton.SetActive(false);
        }

        p1EnergyText.text = p1Energy.ToString();
    }
}
