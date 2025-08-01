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

    public int finalCardCost;
    public int timer;
    public int spacesBehindP1;
    public int spacesBehindP2;
    public TMP_Text timerText;
    public Player p1, p2;
    public bool discarding;
    public bool cutscene = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = 10;

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
                card = playedCard.GetComponent<DisplayCard>().card;
            }

    }


    public void EndInteraction()
    {
        //return to neutral
        cutscene = false;
        p1.isBlockingHigh = false;
        p1.isBlockingLow = false;
        p1.isDodging = false;
        p1.isMoving = false;
        p1.isPushing = false;
        p2.isBlockingHigh = false;
        p2.isBlockingLow = false;
        p2.isDodging = false;
        p2.isMoving = false;
        p2.isPushing = false;
        p1.isHit = false;
        p2.isHit = false;
        p1.dragonInstall--;
        p2.dragonInstall--;
        p1.forceBreak--;
        p2.forceBreak--;
        Debug.Log("Return to Neutral");

        if(p1.health <= 0)
        {
            Debug.Log("Player 2 Wins");
        }
        if(p2.health <= 0)
        {
            Debug.Log("Player 1 Wins");
        }


        //if both players run out of energy, move on to the next round
        if (p1.energy == 0 && p2.energy == 0)
        {
            timer--;
            timerText.text = "" + timer;
            //if the timer gets to 0, game over
            if (timer == 0)
            {
                if (p1.health > p2.health)
                {
                    Debug.Log("Player 1 Wins");
                }
                else if (p1.health < p2.health)
                {
                    Debug.Log("Player 2 Wins");
                }
                else if (p1.health == p2.health)
                {
                    Debug.Log("Draw");
                }
            }
            else
            {
                //if the timer's not 0, refill energy
                if(p1.flipCheck < 0)
                {
                    spacesBehindP1 = p1.position - 1;
                    spacesBehindP2 = 9 - p2.position;
                }
                else
                {
                    spacesBehindP1 = 9 - p1.position;
                    spacesBehindP2 = p2.position - 1;
                }
                if(spacesBehindP1 > 4)
                {
                    spacesBehindP1 = 4;
                }
                if (spacesBehindP2 > 4)
                {
                    spacesBehindP2 = 4;
                }
                p1.energy = 3 + spacesBehindP1;
                p1.energyText.text = "" + p1.energy;
                p2.energy = 3 + spacesBehindP2;
                p2.energyText.text = "" + p2.energy;
            }
        }
    }

    public void LockIn()
    {
        if (discarding == true)
        { 
            if(card.type == "Basic Defense" || card.type == "Basic Movement")
            {
                playedCard.transform.SetParent(hand);
                playedCard.transform.localScale = new Vector3(2.5f, 3.5f, 0);
                playedCard = null;
                lockInButton.SetActive(false);
                return;
            }
            DisplayCard discardingCard = playedCard.GetComponent<DisplayCard>();
            discardingCard.card = null;
            playedCard.transform.SetParent(hand);
            playedCard.transform.localScale = new Vector3(2.5f, 3.5f, 0);
            playedCard = null;
            lockInButton.SetActive(false);
            discarding = false;
            return;
        }
        //calculates actual cost of the card with plus frames factored in
        finalCardCost = card.cost - p1.plusFrames;

        //sets both players plus frames to 0
        p1.plusFrames = 0;
        p2.plusFrames = 0;
        p1.plusFramesText.text = "";
        p2.plusFramesText.text = "";

        //makes sure the card doesn't cost a negative number
        if (finalCardCost < 0)
        {
            finalCardCost = 0;
        }

        //if you have less energy than your opponent or 0 energy, basic cards cost 0
        if((card.type == "Basic Defense" || card.type == "Basic Movement") && (p1.energy < p2.energy || p1.energy == 0))
        {
            Debug.Log("" + card.cardName + " No Cost");
            p1.StartCoroutine("RevealCards");
        }
        //otherwise, if you can afford the card, play it
        else if(finalCardCost <= p1.energy)
        {
            p1.energy = p1.energy - finalCardCost;
            p1.energyText.text = "" + p1.energy;
            Debug.Log("" + card.cardName);
            p1.StartCoroutine("RevealCards");
        }
        //if you can't afford the card, you do nothing, which costs 1 energy if you have more energy than your opponent
        else if(finalCardCost > p1.energy)
        {
            if(p1.energy >= p2.energy)
            {
                p1.energy--;
                p1.energyText.text = "" + p1.energy;
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
  
    }

    
}
