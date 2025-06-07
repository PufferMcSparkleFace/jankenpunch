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
    public int spacesBehindP1;
    public int spacesBehindP2;
    public TMP_Text timerText;
    public TMP_Text p1HealthText;
    public TMP_Text p1EnergyText;
    public TMP_Text p1PlusFramesText;
    public TMP_Text p2HealthText;
    public TMP_Text p2EnergyText;
    public TMP_Text p2PlusFramesText;
    public Player p1, p2;
    public int distance = 4;
    public bool discarding;

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

    IEnumerator FrameDelay()
    {
        yield return new WaitForSeconds(finalCardCost * 0.1f);
        PlayCard();
    }

    public void PlayCard()
    {
        if (card.cardName == "Block High")
        {
            p1.isBlockingHigh = true;
            Debug.Log("Blocking High");
            if (p2.isDodging == true)
            {
                if(p1.character.cardName == "Rynox")
                {
                    p1PlusFrames = p1PlusFrames + 4;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
                else
                {
                    p1PlusFrames = p1PlusFrames + 3;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
            }
        }
        else if (card.cardName == "Block Low")
        {
            p1.isBlockingLow = true;
            Debug.Log("Blocking Low");
            if (p2.isDodging == true)
            {
                if (p1.character.cardName == "Rynox")
                {
                    p1PlusFrames = p1PlusFrames + 4;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
                else
                {
                    p1PlusFrames = p1PlusFrames + 3;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
            }
        }
        else if (card.cardName == "Dodge")
        {
            p1.isDodging = true;
            Debug.Log("Dodging");
            if (p2.isDodging == true)
            {
                if (p1.character.cardName == "Rynox")
                {
                    p1PlusFrames = p1PlusFrames + 4;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
                else
                {
                    p1PlusFrames = p1PlusFrames + 3;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
            }
        }
        else if (card.cardName == "Dash Forward")
        {
            p1.isMoving = true;
            p1.Move(1);
            if (p2.isDodging == true)
            {
                if (p1.character.cardName == "Rynox")
                {
                    p1PlusFrames = p1PlusFrames + 4;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
                else
                {
                    p1PlusFrames = p1PlusFrames + 3;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
            }
        }
        else if (card.cardName == "Dash Back")
        {
            p1.isMoving = true;
            p1.Move(-1);
            if (p2.isDodging == true)
            {
                if (p1.character.cardName == "Rynox")
                {
                    p1PlusFrames = p1PlusFrames + 4;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
                else
                {
                    p1PlusFrames = p1PlusFrames + 3;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
            }
        }
        else if (card.cardName == "Dash Attack")
        {
            p1.Move(2);
            Strike();
        }
        else if (card.cardName == "Hop Kick")
        {
            p1.Move(1);
            Strike();
        }
        else if (card.cardName == "Tackle")
        {
            p1.Move(1);
            Throw();
        }
        else if (card.cardName == "CHARGE!!!")
        {
            p1.isBlockingHigh = true;
            p1.isPushing = true;
            p1.Move(2);
            if (p2.isDodging == true)
            {
                if (p1.character.cardName == "Rynox")
                {
                    p1PlusFrames = p1PlusFrames + 4;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
                else
                {
                    p1PlusFrames = p1PlusFrames + 3;
                    p1PlusFramesText.text = "+" + p1PlusFrames;
                }
            }
        }
        else if (card.cardName == "Warp")
        {
            if(p2.position != 1 && p2.position != 9)
            {
                if(p2.flipCheck < 0)
                {
                    p1.transform.position = p1.stagePositions[p2.position - 2].transform.position;
                    p1.position = p2.position - 1;
                    distance = Mathf.Abs(p1.position - p2.position);
                    p1.flipCheck = -1;
                }
                if(p2.flipCheck > 0)
                {
                    p1.transform.position = p1.stagePositions[p2.position].transform.position;
                    p1.position = p2.position + 1;
                    distance = Mathf.Abs(p1.position - p2.position);
                    p1.flipCheck = 1;
                }

                p1.Move(-3);
                if (p2.isDodging == true)
                {
                    if (p1.character.cardName == "Rynox")
                    {
                        p1PlusFrames = p1PlusFrames + 4;
                        p1PlusFramesText.text = "+" + p1PlusFrames;
                    }
                    else
                    {
                        p1PlusFrames = p1PlusFrames + 3;
                        p1PlusFramesText.text = "+" + p1PlusFrames;
                    }
                }
            }
        }
        else if (card.cardName == "YOU SCARED, BUD?!")
        {
            //replace this with "if opponent.card = a block, you're +5, if they played a dodge you're +9"
            if (p2.isBlockingHigh == true || p2.isBlockingLow == true)
            {
                p1PlusFrames = p1PlusFrames + 5;
                p1PlusFramesText.text = "+" + p1PlusFrames;
            }
            else if (p2.isDodging == true)
            {
                p1PlusFrames = p1PlusFrames + 9;
                p1PlusFramesText.text = "+" + p1PlusFrames;
            }
         
        }
        else if(card.cardName == "Force Choke")
        {
            if (card.range < distance || p2.isDodging == true)
            {
                Debug.Log("Whiff!");
                p2PlusFrames = p2PlusFrames + Mathf.Abs(card.onWhiff);
                p2PlusFramesText.text = "+" + p2PlusFrames;
            }
            else
            {
                Debug.Log("Hit!");
                p2Health = p2Health - card.damage;
                p2HealthText.text = "" + p2Health;
                p1PlusFrames = p1PlusFrames + card.onHit;
                p1PlusFramesText.text = "+" + p1PlusFrames;
                p2Energy = p2Energy - card.cost;
                if (p2Energy < 0)
                {
                    p2Energy = 0;
                }
                p2EnergyText.text = "" + p2Energy;
                p2.Move(-1);
            }
        }
        else if (card.subtype == "Strike")
        {
            Strike();
        }
        else if (card.subtype == "Throw")
        {
            Throw();
        }
        else if (card.subtype == "Projectile")
        {
            Projectile();
        }
        //check if the other person is still resolving an attack, if not, end interaction
        EndInteraction();
    }

    public void Strike()
    {
        if (card.range < distance || p2.isDodging == true)
        {
            Debug.Log("Whiff!");
            p2PlusFrames = p2PlusFrames + Mathf.Abs(card.onWhiff);
            p2PlusFramesText.text = "+" + p2PlusFrames;
        }
        if (card.range >= distance)
        {
            if (p2.isBlockingHigh == true && (card.guard == "High" || card.guard == "Mid"))
            {
                Debug.Log("Blocked!");
                p2PlusFrames = p2PlusFrames + Mathf.Abs(card.onBlock);
                p2PlusFramesText.text = "+" + p2PlusFrames;
                p1Energy = p1Energy + card.cost;
                p1EnergyText.text = "" + p1Energy;
                return;
            }
            if (p2.isBlockingLow == true && (card.guard == "Low" || card.guard == "Mid"))
            {
                Debug.Log("Blocked!");
                p2PlusFrames = p2PlusFrames + Mathf.Abs(card.onBlock);
                p2PlusFramesText.text = "+" + p2PlusFrames;
                p1Energy = p1Energy + card.cost;
                p1EnergyText.text = "" + p1Energy;
                return;
            }
            Debug.Log("Hit!");
            p2Health = p2Health - card.damage;
            p2HealthText.text = "" + p2Health;
            p1PlusFrames = p1PlusFrames + card.onHit;
            p1PlusFramesText.text = "+" + p1PlusFrames;
            p1.isPushing = true;
            p1.Move(1);
            if(card.cardName == "Flash Kick")
            {
                p2.Move(-3);
            }
            //hits and you knock them back 1 while moving forward 1
        }

    }

    public void Throw()
    {
        if(card.range < distance || p2.isDodging == true)
        {
            Debug.Log("Whiff!");
            p2PlusFrames = p2PlusFrames + Mathf.Abs(card.onWhiff);
            p2PlusFramesText.text = "+" + p2PlusFrames;
        }
        else
        {
            Debug.Log("Hit!");
            p2Health = p2Health - card.damage;
            p2HealthText.text = "" + p2Health;
            p1PlusFrames = p1PlusFrames + card.onHit;
            p1PlusFramesText.text = "+" + p1PlusFrames;
            if(card.cardName == "Grab")
            {
                if(p1.flipCheck < 0)
                {
                    p1.transform.position = p1.stagePositions[p2.position -1].transform.position;
                    p1.position = p2.position;
                    p2.transform.position = p2.stagePositions[p1.position -2].transform.position;
                    p2.position = p1.position - 1;
                }
                else
                {
                    p1.transform.position = p1.stagePositions[p2.position - 1].transform.position;
                    p1.position = p2.position;
                    p2.transform.position = p2.stagePositions[p1.position].transform.position;
                    p2.position = p1.position + 1;
                }
            }
        }
    }

    public void Projectile()
    {
        if (card.range < distance || p2.isDodging == true)
        {
            Debug.Log("Whiff!");
            p2PlusFrames = p2PlusFrames + Mathf.Abs(card.onWhiff);
            p2PlusFramesText.text = "+" + p2PlusFrames;
        }
        if(card.range >= distance)
        {
            if(p2.isBlockingHigh == true && (card.guard == "High" || card.guard == "Mid"))
            {
                Debug.Log("Blocked!");
                p2PlusFrames = p2PlusFrames + Mathf.Abs(card.onBlock);
                p2PlusFramesText.text = "+" + p2PlusFrames;
                return;
            }
            if(p2.isBlockingLow == true && (card.guard == "Low" || card.guard == "Mid"))
            {
                Debug.Log("Blocked!");
                p2PlusFrames = p2PlusFrames + Mathf.Abs(card.onBlock);
                p2PlusFramesText.text = "+" + p2PlusFrames;
                return;
            }
            Debug.Log("Hit!");
            p2Health = p2Health - card.damage;
            p2HealthText.text = "" + p2Health;
            p1PlusFrames = p1PlusFrames + card.onHit;
            p1PlusFramesText.text = "+" + p1PlusFrames;
            p2Energy = p2Energy - card.cost;
            if(p2Energy < 0)
            {
                p2Energy = 0;
            }
            p2EnergyText.text = "" + p2Energy;
            p2.Move(-1);
        }
    }


    public void EndInteraction()
    {
        //return to neutral
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
                p1Energy = 3 + spacesBehindP1;
                p1EnergyText.text = "" + p1Energy;
                p2Energy = 3 + spacesBehindP2;
                p2EnergyText.text = "" + p2Energy;
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
            Debug.Log("" + card.cardName + " No Cost");
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
  
    }

    
}
