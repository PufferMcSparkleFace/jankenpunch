using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Transform otherPlayer;
    public float flipCheck;
    public GameObject[] stagePositions;
    public int position;
    public bool isBlockingHigh, isBlockingLow, isDodging, isMoving, isPushing, isHit = false;
    public int unitsActual;
    public GameManager gameManager;
    public Player opponent;
    public CharacterCards character;
    public TMP_Text installText;
    public int dragonInstall = 0;
    public int forceBreak = 0;
    public bool empowered = false;
    public int distance = 4;
    public GameObject revealedCard;
    public DisplayCard revealedCardScript;

    public int health, energy, plusFrames;
    public TMP_Text healthText, energyText, plusFramesText;

    public Button abilityOneButton, abilityTwoButton;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "P1")
        {
            this.transform.position = stagePositions[2].transform.position;
            position = 3;
        }
        else
        {
            this.transform.position = stagePositions[6].transform.position;
            position = 7;
        }
        installText.text = "";
        healthText.text = health.ToString();
        energyText.text = energy.ToString();
        plusFramesText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        flipCheck = this.transform.position.x - otherPlayer.position.x;
        if(flipCheck > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (plusFrames == 0)
        {
            abilityOneButton.gameObject.SetActive(false);
        }
        if(plusFrames <= 2)
        {
            abilityTwoButton.gameObject.SetActive(false);
        }
        if (plusFrames >= 1)
        {
            abilityOneButton.gameObject.SetActive(true);
        }
        if (plusFrames >= 3)
        {
            abilityTwoButton.gameObject.SetActive(true);
        }
        if (gameManager.discarding == true)
        {
            abilityOneButton.gameObject.SetActive(false);
        }
        if (empowered == true || dragonInstall > 0 || forceBreak > 0)
        {
            abilityTwoButton.gameObject.SetActive(false);
        }
        if (empowered == false && dragonInstall <= 0 && forceBreak <= 0)
        {
            installText.text = "";
        }
        if(dragonInstall > 0)
        {
            installText.text = "Dragon Install: " + dragonInstall;
        }
        if (forceBreak > 0)
        {
            installText.text = "Force Break: " + forceBreak;
        }
        if (plusFrames == 0)
        {
            plusFramesText.text = "";
        }
    }

    public void Move(int units)
    {
        Debug.Log("Move " + units.ToString() + " units");

        for (int i = 0; i < Mathf.Abs(units); i++)
        {
            //if you're facing right
            if (flipCheck < 0)
            {
                if (units > 0)
                {
                    unitsActual = 1;
                }
                else
                {
                    unitsActual = -1;
                }
                if (unitsActual > 0 && distance == 1)
                {
                    if(isPushing == false)
                    {
                        return;
                    }
                    else
                    {
                        if (opponent.position == 9 || opponent.position == 1)
                        {
                            return;
                        }
                        opponent.transform.position = stagePositions[opponent.position].transform.position;
                        opponent.position++;
                    }

                }
                this.transform.position = stagePositions[(position - 1) + unitsActual].transform.position;
                position = position + unitsActual;
                distance = Mathf.Abs(position - opponent.position);
            }
            //if you're facing left
            else
            {
                if(units > 0)
                {
                    unitsActual = -1;
                }
                else
                {
                    unitsActual = 1;
                }
                if (unitsActual < 0 && distance == 1)
                {
                    if (isPushing == false)
                    {
                        return;
                    }
                    else
                    {
                        if(opponent.position == 9 || opponent.position == 1)
                        {
                            return;
                        }
                        opponent.transform.position = stagePositions[opponent.position -2].transform.position;
                        opponent.position--;
                    }
                }
                this.transform.position = stagePositions[(position - 1) + unitsActual].transform.position;
                position = position + unitsActual;
                distance = Mathf.Abs(position - opponent.position);
            }
        }

    }

    public void AbilityOne()
    {
        if(gameManager.discarding == true)
        {
            return;
        }
        plusFrames--;
        plusFramesText.text = "+" + plusFrames;
        if(plusFrames == 0)
        {
            plusFramesText.text = "";
        }
        if(character.cardName == "Zyla")
        {
            Debug.Log("Zyla +1");
            Move(1);
        }
        if (character.cardName == "Taibo")
        {
            Debug.Log("Taibo +1");
            gameManager.discarding = true;
        }
        if (character.cardName == "Rynox")
        {
            Debug.Log("Rynox +1");
            energy++;
            energyText.text = "" + energy;
        }
    }

    public void AbilityTwo()
    {
        plusFrames = plusFrames -3;
        plusFramesText.text = "+" + plusFrames;
        if (plusFrames == 0)
        {
            plusFramesText.text = "";
        }

        if (character.cardName == "Zyla")
        {
            dragonInstall = 5;
            Debug.Log("Zyla +3");
        }
        if (character.cardName == "Taibo")
        {
            forceBreak = 5;
            Debug.Log("Taibo +3");
        }
        if (character.cardName == "Rynox")
        {
            empowered = true;
            installText.text = "Empowered";
            Debug.Log("Rynox +3");
        }
    }

    IEnumerator RevealCards()
    {
        revealedCardScript.card = gameManager.card;
        revealedCardScript.UpdateCard();
        revealedCard.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        revealedCard.SetActive(false);
        StartCoroutine("FrameDelay");
    }
    
    IEnumerator FrameDelay()
    {
        yield return new WaitForSeconds(gameManager.finalCardCost * 0.1f);
        PlayCard();
    }

    public void PlayCard()
    {
        if (isHit == true)
        {
            gameManager.EndInteraction();
            return;
        }

        if (gameManager.card.cardName == "Block High")
        {
            isBlockingHigh = true;
            Debug.Log("Blocking High");
            if (opponent.isDodging == true)
            {
                if (character.cardName == "Rynox")
                {
                    plusFrames = plusFrames + 4;
                    plusFramesText.text = "+" + plusFrames;
                }
                else
                {
                    plusFrames = plusFrames + 3;
                    plusFramesText.text = "+" + plusFrames;
                }
            }
        }
        else if (gameManager.card.cardName == "Block Low")
        {
            isBlockingLow = true;
            Debug.Log("Blocking Low");
            if (opponent.isDodging == true)
            {
                if (character.cardName == "Rynox")
                {
                    plusFrames = plusFrames + 4;
                    plusFramesText.text = "+" + plusFrames;
                }
                else
                {
                    plusFrames = plusFrames + 3;
                    plusFramesText.text = "+" + plusFrames;
                }
            }
        }
        else if (gameManager.card.cardName == "Dodge")
        {
            isDodging = true;
            Debug.Log("Dodging");
        }
        else if (gameManager.card.cardName == "Dash Forward")
        {
            isMoving = true;
            Move(1);
            if (opponent.isDodging == true)
            {
                if (character.cardName == "Rynox")
                {
                    plusFrames = plusFrames + 4;
                    plusFramesText.text = "+" + plusFrames;
                }
                else
                {
                    plusFrames = plusFrames + 3;
                    plusFramesText.text = "+" + plusFrames;
                }
            }
        }
        else if (gameManager.card.cardName == "Dash Back")
        {
            isMoving = true;
            Move(-1);
            if (opponent.isDodging == true)
            {
                if (character.cardName == "Rynox")
                {
                    plusFrames = plusFrames + 4;
                    plusFramesText.text = "+" + plusFrames;
                }
                else
                {
                    plusFrames = plusFrames + 3;
                    plusFramesText.text = "+" + plusFrames;
                }
            }
        }
        else if (gameManager.card.cardName == "Dash Attack")
        {
            Move(2);
            Strike();
        }
        else if (gameManager.card.cardName == "Hop Kick")
        {
            Move(1);
            Strike();
        }
        else if (gameManager.card.cardName == "Tackle")
        {
            Move(1);
            Throw();
        }
        else if (gameManager.card.cardName == "CHARGE!!!")
        {
            isBlockingHigh = true;
            isPushing = true;
            Move(2);
            if (opponent.isDodging == true)
            {
                if (character.cardName == "Rynox")
                {
                    plusFrames = plusFrames + 4;
                    plusFramesText.text = "+" + plusFrames;
                }
                else
                {
                    plusFrames = plusFrames + 3;
                    plusFramesText.text = "+" + plusFrames;
                }
            }
        }
        else if (gameManager.card.cardName == "Warp")
        {
            if (opponent.position != 1 && opponent.position != 9)
            {
                if (opponent.flipCheck < 0)
                {
                    transform.position = stagePositions[opponent.position - 2].transform.position;
                    position = opponent.position - 1;
                    distance = Mathf.Abs(position - opponent.position);
                    flipCheck = -1;
                }
                if (opponent.flipCheck > 0)
                {
                    transform.position = stagePositions[opponent.position].transform.position;
                    position = opponent.position + 1;
                    distance = Mathf.Abs(position - opponent.position);
                    flipCheck = 1;
                }

                Move(-3);
                if (opponent.isDodging == true)
                {
                    if (character.cardName == "Rynox")
                    {
                        plusFrames = plusFrames + 4;
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        plusFrames = plusFrames + 3;
                        plusFramesText.text = "+" + plusFrames;
                    }
                }
            }
        }
        else if (gameManager.card.cardName == "YOU SCARED, BUD?!")
        {
            //replace this with "if opponent.card = a block, you're +5, if they played a dodge you're +9"
            if (opponent.isBlockingHigh == true || opponent.isBlockingLow == true)
            {
                plusFrames = plusFrames + 5;
                plusFramesText.text = "+" + plusFrames;
            }
            else if (opponent.isDodging == true)
            {
                plusFrames = plusFrames + 9;
                plusFramesText.text = "+" + plusFrames;
            }

        }
        else if (gameManager.card.cardName == "Force Choke")
        {
            if (forceBreak > 0)
            {
                gameManager.card.range++;
            }
            if (gameManager.card.range < distance || opponent.isDodging == true)
            {
                Debug.Log("Whiff!");
                opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onWhiff);
                opponent.plusFramesText.text = "+" + opponent.plusFrames;
            }
            else
            {
                if (forceBreak > 0)
                {
                    opponent.health = opponent.health - (gameManager.card.damage + 1);
                    opponent.healthText.text = "" + opponent.health;
                }
                else
                {
                    opponent.health = opponent.health - gameManager.card.damage;
                    opponent.healthText.text = "" + opponent.health;
                }
                Debug.Log("Hit!");
                plusFrames = plusFrames + gameManager.card.onHit;
                plusFramesText.text = "+" + plusFrames;
                opponent.energy = opponent.energy - gameManager.card.cost;
                opponent.isHit = true;
                if (opponent.energy < 0)
                {
                    opponent.energy = 0;
                }
                opponent.energyText.text = "" + opponent.energy;
                if (forceBreak > 0)
                {
                    opponent.Move(-2);
                }
                else
                {
                    opponent.Move(-1);
                }

            }
        }
        else if (gameManager.card.subtype == "Strike")
        {
            Strike();
        }
        else if (gameManager.card.subtype == "Throw")
        {
            Throw();
        }
        else if (gameManager.card.subtype == "Projectile")
        {
            Projectile();
        }
        //check if the other person is still resolving an attack, if not, end interaction
        gameManager.EndInteraction();
    }

    public void Strike()
    {
        if (gameManager.card.range < distance || opponent.isDodging == true)
        {
            Debug.Log("Whiff!");
            opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onWhiff);
            opponent.plusFramesText.text = "+" + opponent.plusFrames;
        }
        if (gameManager.card.range >= distance && opponent.isDodging == false)
        {
            if (opponent.isBlockingHigh == true && (gameManager.card.guard == "High" || gameManager.card.guard == "Mid"))
            {
                if (dragonInstall > 0)
                {
                    Debug.Log("Blocked!");
                    if (gameManager.card.onBlock + 1 >= 0)
                    {
                        plusFrames = plusFrames + Mathf.Abs(gameManager.card.onBlock + 1);
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onBlock + 1);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                    }
                    energy = energy + gameManager.card.cost;
                    energyText.text = "" + energy;
                    return;
                }
                else
                {
                    Debug.Log("Blocked!");
                    if (gameManager.card.onBlock >= 0)
                    {
                        plusFrames = plusFrames + Mathf.Abs(gameManager.card.onBlock);
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onBlock);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                    }
                    energy = energy + gameManager.card.cost;
                    energyText.text = "" + energy;
                    return;
                }

            }
            if (opponent.isBlockingLow == true && (gameManager.card.guard == "Low" || gameManager.card.guard == "Mid"))
            {
                if (dragonInstall > 0)
                {
                    if (gameManager.card.onBlock + 1 >= 0)
                    {
                        plusFrames = plusFrames + Mathf.Abs(gameManager.card.onBlock + 1);
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onBlock + 1);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                    }
                    Debug.Log("Blocked!");
                    energy = energy + gameManager.card.cost;
                    energyText.text = "" + energy;
                    return;
                }
                else
                {
                    Debug.Log("Blocked!");
                    if (gameManager.card.onBlock >= 0)
                    {
                        plusFrames = plusFrames + Mathf.Abs(gameManager.card.onBlock);
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onBlock);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                    }
                    energy = energy + gameManager.card.cost;
                    energyText.text = "" + energy;
                    return;
                }

            }
            if (character.cardName == "Zyla" && (opponent.isBlockingLow == true && gameManager.card.guard == "High") || (opponent.isBlockingHigh == true && gameManager.card.guard == "Low"))
            {
                if (dragonInstall > 0)
                {
                    Debug.Log("Get Mixed!");
                    opponent.health = opponent.health - (gameManager.card.damage + 2);
                    opponent.healthText.text = "" + opponent.health;
                    plusFrames = plusFrames + gameManager.card.onHit + 1;
                    plusFramesText.text = "+" + plusFrames;
                    isPushing = true;
                    Move(1);
                    opponent.isHit = true;
                    return;
                }
                else
                {
                    Debug.Log("Get Mixed!");
                    opponent.health = opponent.health - gameManager.card.damage;
                    opponent.healthText.text = "" + opponent.health;
                    plusFrames = plusFrames + gameManager.card.onHit + 1;
                    plusFramesText.text = "+" + plusFrames;
                    isPushing = true;
                    Move(1);
                    opponent.isHit = true;
                    return;
                }

            }
            if (dragonInstall > 0)
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - (gameManager.card.damage + 2);
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + gameManager.card.onHit;
                plusFramesText.text = "+" + plusFrames;
                isPushing = true;
                Move(1);
                opponent.isHit = true;
            }
            else
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - gameManager.card.damage;
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + gameManager.card.onHit;
                plusFramesText.text = "+" + plusFrames;
                isPushing = true;
                Move(1);
                opponent.isHit = true;
            }

            if (gameManager.card.cardName == "Flash Kick")
            {
                opponent.Move(-3);
            }
        }

    }

    public void Throw()
    {
        if (gameManager.card.range < distance || opponent.isDodging == true)
        {
            Debug.Log("Whiff!");
            opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onWhiff);
            opponent.plusFramesText.text = "+" + opponent.plusFrames;
            if (empowered == true)
            {
                empowered = false;
            }
        }
        else
        {
            if (empowered == true)
            {
                Debug.Log("Empowered Hit!");
                opponent.health = opponent.health - (gameManager.card.damage * 3);
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + gameManager.card.onHit;
                plusFramesText.text = "+" + plusFrames;
                empowered = false;
                opponent.isHit = true;
            }
            else
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - gameManager.card.damage;
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + gameManager.card.onHit;
                plusFramesText.text = "+" + plusFrames;
                opponent.isHit = true;
            }
            if (gameManager.card.cardName == "Grab")
            {
                if (flipCheck < 0)
                {
                    transform.position = stagePositions[opponent.position - 1].transform.position;
                    position = opponent.position;
                    opponent.transform.position = opponent.stagePositions[position - 2].transform.position;
                    opponent.position = position - 1;
                }
                else
                {
                    transform.position = stagePositions[opponent.position - 1].transform.position;
                    position = opponent.position;
                    opponent.transform.position = opponent.stagePositions[position].transform.position;
                    opponent.position = position + 1;
                }
            }
        }
    }

    public void Projectile()
    {
        if (forceBreak > 0)
        {
            gameManager.card.range++;
        }
        if (gameManager.card.range < distance || opponent.isDodging == true)
        {
            Debug.Log("Whiff!");
            opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onWhiff);
            opponent.plusFramesText.text = "+" + opponent.plusFrames;
        }
        if (gameManager.card.range >= distance && opponent.isDodging == false)
        {
            if (opponent.isBlockingHigh == true && (gameManager.card.guard == "High" || gameManager.card.guard == "Mid"))
            {
                Debug.Log("Blocked!");
                opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onBlock);
                opponent.plusFramesText.text = "+" + opponent.plusFrames;
                return;
            }
            if (opponent.isBlockingLow == true && (gameManager.card.guard == "Low" || gameManager.card.guard == "Mid"))
            {
                Debug.Log("Blocked!");
                opponent.plusFrames = opponent.plusFrames + Mathf.Abs(gameManager.card.onBlock);
                opponent.plusFramesText.text = "+" + opponent.plusFrames;
                return;
            }
            if (forceBreak > 0)
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - (gameManager.card.damage + 1);
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + gameManager.card.onHit;
                plusFramesText.text = "+" + plusFrames;
                opponent.energy = opponent.energy - gameManager.card.cost;
                opponent.isHit = true;
            }
            else
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - gameManager.card.damage;
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + gameManager.card.onHit;
                plusFramesText.text = "+" + plusFrames;
                opponent.energy = opponent.energy - gameManager.card.cost;
                opponent.isHit = true;
            }
            if (opponent.energy < 0)
            {
                opponent.energy = 0;
            }
            opponent.energyText.text = "" + opponent.energy;
            if (character.cardName == "Taibo")
            {
                if (forceBreak > 0)
                {
                    opponent.Move(-2);
                }
                else
                {
                    opponent.Move(-1);
                }

            }
        }
    }
}
