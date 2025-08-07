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
    public Player me, opponent;
    public GameObject meGameObject, opponentGameObject;
    public bool discarding;
    public bool cutscene = false;
    public bool isPlayer2 = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = 10;

        hand = GameObject.FindGameObjectWithTag("Hand").transform;
        if(isPlayer2 == false)
        {
            meGameObject = GameObject.FindGameObjectWithTag("P1");
            me = meGameObject.GetComponent<Player>();
            opponentGameObject = GameObject.FindGameObjectWithTag("P2");
            opponent = opponentGameObject.GetComponent<Player>();
        }
        else
        {
            meGameObject = GameObject.FindGameObjectWithTag("P2");
            me = meGameObject.GetComponent<Player>();
            opponentGameObject = GameObject.FindGameObjectWithTag("P1");
            opponent = opponentGameObject.GetComponent<Player>();
        }
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
        me.isBlockingHigh = false;
        me.isBlockingLow = false;
        me.isDodging = false;
        me.isMoving = false;
        me.isPushing = false;
        opponent.isBlockingHigh = false;
        opponent.isBlockingLow = false;
        opponent.isDodging = false;
        opponent.isMoving = false;
        opponent.isPushing = false;
        me.isHit = false;
        opponent.isHit = false;
        me.dragonInstall--;
        opponent.dragonInstall--;
        me.forceBreak--;
        opponent.forceBreak--;
        me.playedCard = null;
        opponent.playedCard = null;
        Debug.Log("Return to Neutral");

        if(me.health <= 0)
        {
            Debug.Log("Player 2 Wins");
        }
        if(opponent.health <= 0)
        {
            Debug.Log("Player 1 Wins");
        }


        //if both players run out of energy, move on to the next round
        if (me.energy == 0 && opponent.energy == 0)
        {
            timer--;
            timerText.text = "" + timer;
            //if the timer gets to 0, game over
            if (timer == 0)
            {
                if (me.health > opponent.health)
                {
                    Debug.Log("Player 1 Wins");
                }
                else if (me.health < opponent.health)
                {
                    Debug.Log("Player 2 Wins");
                }
                else if (me.health == opponent.health)
                {
                    Debug.Log("Draw");
                }
            }
            else
            {
                //if the timer's not 0, refill energy
                if(me.flipCheck < 0)
                {
                    spacesBehindP1 = me.position - 1;
                    spacesBehindP2 = 9 - opponent.position;
                }
                else
                {
                    spacesBehindP1 = 9 - me.position;
                    spacesBehindP2 = opponent.position - 1;
                }
                if(spacesBehindP1 > 4)
                {
                    spacesBehindP1 = 4;
                }
                if (spacesBehindP2 > 4)
                {
                    spacesBehindP2 = 4;
                }
                me.energy = 3 + spacesBehindP1;
                me.energyText.text = "" + me.energy;
                opponent.energy = 3 + spacesBehindP2;
                opponent.energyText.text = "" + opponent.energy;
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
        finalCardCost = card.cost - me.plusFrames;

        //sets both players plus frames to 0
        me.plusFrames = 0;
        opponent.plusFrames = 0;
        me.plusFramesText.text = "";
        opponent.plusFramesText.text = "";

        //makes sure the card doesn't cost a negative number
        if (finalCardCost < 0)
        {
            finalCardCost = 0;
        }

        //if you have less energy than your opponent or 0 energy, basic cards cost 0
        if((card.type == "Basic Defense" || card.type == "Basic Movement") && (me.energy < opponent.energy || me.energy == 0))
        {
            Debug.Log("" + card.cardName + " No Cost");
            me.StartCoroutine("WaitForOpponent");
        }
        //otherwise, if you can afford the card, play it
        else if(finalCardCost <= me.energy)
        {
            me.energy = me.energy - finalCardCost;
            me.energyText.text = "" + me.energy;
            Debug.Log("" + card.cardName);
            me.StartCoroutine("WaitForOpponent");
        }
        //if you can't afford the card, you do nothing, which costs 1 energy if you have more energy than your opponent
        else if(finalCardCost > me.energy)
        {
            if(me.energy >= opponent.energy)
            {
                me.energy--;
                me.energyText.text = "" + me.energy;
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
