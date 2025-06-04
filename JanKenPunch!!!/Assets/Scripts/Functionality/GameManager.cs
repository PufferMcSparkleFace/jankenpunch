using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform playArea;
    public GameObject lockInButton;
    public GameObject playedCard;
    public NonBasicCard card;

    public int turnTimer;
    public TMP_Text turnTimerText;

    public Transform hand = null;

    public Draw deck;

    public int p1Health;
    public int p1Energy;
    public int p1PlusFrames;
    public int p2Health;
    public int p2Energy;
    public int p2PlusFrames;
    public int finalCardCost;
    public int timer;
    public TMP_Text timerText;
    public TMP_Text p1HealthText;
    public TMP_Text p1EnergyText;
    public TMP_Text p1PlusFramesText;
    public TMP_Text p2HealthText;
    public TMP_Text p2EnergyText;
    public TMP_Text p2PlusFramesText;
    public Player p1, p2;
    public int distance;

    // Start is called before the first frame update
    void Start()
    {
        timer = 10;
        p1HealthText.text = p1Health.ToString();
        p1EnergyText.text = p1Energy.ToString();
        p2HealthText.text = p2Health.ToString();
        p2EnergyText.text = p2Energy.ToString();
        p1PlusFramesText.text = "";
        p2PlusFramesText.text = "";

        hand = GameObject.FindGameObjectWithTag("Hand").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(p1.position - p2.position);

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
                card = playedCard.GetComponent<DisplayCard>().card;
            }

    }

    public void LockIn()
    {

        //calculates actual cost of the card with plus frames factored in
        finalCardCost = card.cost - p1PlusFrames;

        //sets both players plus frames to 0
        p1PlusFrames = 0;
        p2PlusFrames = 0;
        p1PlusFramesText.text = "";
        p2PlusFramesText.text = "";

        //makes sure the card doesn't cost a negative number
        if (finalCardCost < 0)
        {
            finalCardCost = 0;
        }

        //if you have less energy than your opponent or 0 energy, basic cards cost 0
        if((card.type == "Basic Defense" || card.type == "Basic Movement") && (p1Energy < p2Energy || p1Energy == 0))
        {
            Debug.Log("" + card.cardName);
            StartCoroutine("FrameDelay");
        }
        //otherwise, if you can afford the card, play it
        else if(finalCardCost <= p1Energy)
        {
            p1Energy = p1Energy - finalCardCost;
            p1EnergyText.text = "" + p1Energy;
            Debug.Log("" + card.cardName);
            StartCoroutine("FrameDelay");
        }
        //if you can't afford the card, you do nothing, which costs 1 energy if you have more energy than your opponent
        else if(finalCardCost > p1Energy)
        {
            if(p1Energy >= p2Energy)
            {
                p1Energy--;
                p1EnergyText.text = "" + p1Energy;
            }
            Debug.Log("Do Nothing");
        }


        //if you play a nonbasic card, set the card to null (so that it gets replaced)
        if ((card.type != "Basic Defense" && card.type != "Basic Movement"))
        {
            DisplayCard card = playedCard.GetComponent<DisplayCard>();
            card.card = null;
        }

        //returns card to your hand and turns off the lock in ui
        playedCard.transform.SetParent(hand);
        playedCard.transform.localScale = new Vector3(2.5f, 3.5f, 0);
        playedCard = null;
        lockInButton.SetActive(false);

        //return to neutral
        p1.isBlockingHigh = false;
        p1.isBlockingLow = false;
        p1.isDodging = false;
        p2.isBlockingHigh = false;
        p2.isBlockingLow = false;
        p2.isDodging = false;
        Debug.Log("Return to Neutral");


        //if both players run out of energy, move on to the next round
        if (p1Energy == 0 && p2Energy == 0)
        {
            timer--;
            timerText.text = "" + timer;
            //if the timer gets to 0, game over
            if (timer == 0)
            {
                if (p1Health > p2Health)
                {
                    Debug.Log("Player 1 Wins");
                }
                else if (p1Health < p2Health)
                {
                    Debug.Log("Player 2 Wins");
                }
                else if (p1Health == p2Health)
                {
                    Debug.Log("Draw");
                }
            }
            //if the timer's not 0, refill energy
        }
  
    }

    IEnumerator FrameDelay()
    {
        yield return new WaitForSeconds(finalCardCost * 0.1f);
        PlayCard();
    }

    public void PlayCard()
    {
        if(card.cardName == "Block High")
        {
            p1.isBlockingHigh = true;
            Debug.Log("Blocking High");
        }
    }
}
