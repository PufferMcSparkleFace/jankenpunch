using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int turnTimer;
    public TMP_Text turnTimerText;

    public Draw deck;

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

    
}
