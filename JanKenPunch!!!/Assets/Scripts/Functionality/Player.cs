using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Transform otherPlayer;
    public GameObject otherPlayerGameObject;
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
    public TMP_Text revealedCardCostText;
    public TMP_Text waitingForOpponent;
    public GameObject installTextGameObject, healthTextGameObject, energyTextGameObject, plusFramesTextGameObject,
    revealedCardCostTextGameObject, abilityOneButtonGameObject, abilityTwoButtonGameObject, waitingGameObject,
    gameManagerObject, characterCardGameObject, canvas;


    public DisplayCharacterCard characterCard;

    public NonBasicCard playedCard;

    public int health, energy, plusFrames;
    public TMP_Text healthText, energyText, plusFramesText;

    public Button abilityOneButton, abilityTwoButton;

    // Start is called before the first frame update
    void Start()
    {
        waitingGameObject = GameObject.FindGameObjectWithTag("Waiting");
        waitingForOpponent = waitingGameObject.GetComponent<TMP_Text>();
        gameManagerObject = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        stagePositions[0] = GameObject.FindGameObjectWithTag("SP1");
        stagePositions[1] = GameObject.FindGameObjectWithTag("SP2");
        stagePositions[2] = GameObject.FindGameObjectWithTag("SP3");
        stagePositions[3] = GameObject.FindGameObjectWithTag("SP4");
        stagePositions[4] = GameObject.FindGameObjectWithTag("SP5");
        stagePositions[5] = GameObject.FindGameObjectWithTag("SP6");
        stagePositions[6] = GameObject.FindGameObjectWithTag("SP7");
        stagePositions[7] = GameObject.FindGameObjectWithTag("SP8");
        stagePositions[8] = GameObject.FindGameObjectWithTag("SP9");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.transform.parent = canvas.transform;
        transform.localScale = new Vector3(2, 3, 0);

        if (GameObject.FindGameObjectWithTag("P1") == false)
        {
            this.gameObject.tag = "P1";
            installTextGameObject = GameObject.FindGameObjectWithTag("P1 Install");
            healthTextGameObject = GameObject.FindGameObjectWithTag("P1 Health");
            energyTextGameObject = GameObject.FindGameObjectWithTag("P1 Energy");
            plusFramesTextGameObject = GameObject.FindGameObjectWithTag("P1 Plus Frames");
            installText = installTextGameObject.GetComponent<TMP_Text>();
            healthText = healthTextGameObject.GetComponent<TMP_Text>();
            energyText = energyTextGameObject.GetComponent<TMP_Text>();
            plusFramesText = plusFramesTextGameObject.GetComponent<TMP_Text>();
            revealedCard = GameObject.FindGameObjectWithTag("P1 Revealed Card");
            revealedCardScript = revealedCard.GetComponent<DisplayCard>();
            revealedCardCostTextGameObject = GameObject.FindGameObjectWithTag("P1 Revealed Card Cost");
            revealedCardCostText = revealedCardCostTextGameObject.GetComponent<TMP_Text>();
            abilityOneButtonGameObject = GameObject.FindGameObjectWithTag("P1 Ability 1");
            abilityTwoButtonGameObject = GameObject.FindGameObjectWithTag("P1 Ability 2");
            abilityOneButton = abilityOneButtonGameObject.GetComponent<Button>();
            abilityTwoButton = abilityTwoButtonGameObject.GetComponent<Button>();
            characterCardGameObject = GameObject.FindGameObjectWithTag("P1 Character");
            characterCard = characterCardGameObject.GetComponent<DisplayCharacterCard>();
            revealedCard.SetActive(false);
            position = 3;
            this.transform.position = stagePositions[2].transform.position;
        }
        else
        {
            this.gameObject.tag = "P2";
            installTextGameObject = GameObject.FindGameObjectWithTag("P2 Install");
            healthTextGameObject = GameObject.FindGameObjectWithTag("P2 Health");
            energyTextGameObject = GameObject.FindGameObjectWithTag("P2 Energy");
            plusFramesTextGameObject = GameObject.FindGameObjectWithTag("P2 Plus Frames");
            installText = installTextGameObject.GetComponent<TMP_Text>();
            healthText = healthTextGameObject.GetComponent<TMP_Text>();
            energyText = energyTextGameObject.GetComponent<TMP_Text>();
            plusFramesText = plusFramesTextGameObject.GetComponent<TMP_Text>();
            revealedCard = GameObject.FindGameObjectWithTag("P2 Revealed Card");
            revealedCardScript = revealedCard.GetComponent<DisplayCard>();
            revealedCardCostTextGameObject = GameObject.FindGameObjectWithTag("P2 Revealed Card Cost");
            revealedCardCostText = revealedCardCostTextGameObject.GetComponent<TMP_Text>();
            abilityOneButtonGameObject = GameObject.FindGameObjectWithTag("P2 Ability 1");
            abilityTwoButtonGameObject = GameObject.FindGameObjectWithTag("P2 Ability 2");
            abilityOneButton = abilityOneButtonGameObject.GetComponent<Button>();
            abilityTwoButton = abilityTwoButtonGameObject.GetComponent<Button>();
            revealedCard.SetActive(false);
            otherPlayerGameObject = GameObject.FindGameObjectWithTag("P1");
            otherPlayer = otherPlayerGameObject.GetComponent<Transform>();
            opponent = otherPlayerGameObject.GetComponent<Player>();
            characterCardGameObject = GameObject.FindGameObjectWithTag("P2 Character");
            characterCard = characterCardGameObject.GetComponent<DisplayCharacterCard>();
            characterCard.SetCharacter(2);
            gameManager.SetPlayer();
            opponent.SetOpponent();
            waitingGameObject.SetActive(false);
            position = 7;
            this.transform.position = stagePositions[6].transform.position;
        }

        installText.text = "";
        healthText.text = "30";
        energyText.text = "5";
        plusFramesText.text = "";
    }

    public void SetOpponent()
    {
        otherPlayerGameObject = GameObject.FindGameObjectWithTag("P2");
        otherPlayer = otherPlayerGameObject.GetComponent<Transform>();
        opponent = otherPlayerGameObject.GetComponent<Player>();
        gameManager.SetPlayer();
        characterCard.SetCharacter(1);
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

    IEnumerator WaitForOpponent()
    {
        waitingForOpponent.gameObject.SetActive(true);
        playedCard = gameManager.card;
        while (opponent.playedCard == null)
        {
            yield return null;
        }
        waitingForOpponent.gameObject.SetActive(false);
        StartCoroutine("RevealCards");
    }

    IEnumerator RevealCards()
    {
        gameManager.cutscene = true;
        revealedCardScript.card = playedCard;
        revealedCardScript.UpdateCard();
        if(gameManager.finalCardCost != playedCard.cost)
        {
            revealedCardCostText.text = gameManager.finalCardCost.ToString();
            revealedCardCostText.color = new Color(255, 115, 0, 255); 
        }
        else
        {
            revealedCardCostText.color = new Color(0, 0, 0, 255);
        }
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

        if (playedCard.cardName == "Block High")
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
        else if (playedCard.cardName == "Block Low")
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
        else if (playedCard.cardName == "Dodge")
        {
            isDodging = true;
            Debug.Log("Dodging");
        }
        else if (playedCard.cardName == "Dash Forward")
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
        else if (playedCard.cardName == "Dash Back")
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
        else if (playedCard.cardName == "Dash Attack")
        {
            Move(2);
            Strike();
        }
        else if (playedCard.cardName == "Hop Kick")
        {
            Move(1);
            Strike();
        }
        else if (playedCard.cardName == "Tackle")
        {
            Move(1);
            Throw();
        }
        else if (playedCard.cardName == "CHARGE!!!")
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
        else if (playedCard.cardName == "Warp")
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
        else if (playedCard.cardName == "YOU SCARED, BUD?!")
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
        else if (playedCard.cardName == "Force Choke")
        {
            if (forceBreak > 0)
            {
                playedCard.range++;
            }
            if (playedCard.range < distance || opponent.isDodging == true)
            {
                Debug.Log("Whiff!");
                opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onWhiff);
                opponent.plusFramesText.text = "+" + opponent.plusFrames;
            }
            else
            {
                if (forceBreak > 0)
                {
                    opponent.health = opponent.health - (playedCard.damage + 1);
                    opponent.healthText.text = "" + opponent.health;
                }
                else
                {
                    opponent.health = opponent.health - playedCard.damage;
                    opponent.healthText.text = "" + opponent.health;
                }
                Debug.Log("Hit!");
                plusFrames = plusFrames + playedCard.onHit;
                plusFramesText.text = "+" + plusFrames;
                opponent.energy = opponent.energy - playedCard.cost;
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
        else if (playedCard.subtype == "Strike")
        {
            Strike();
        }
        else if (playedCard.subtype == "Throw")
        {
            Throw();
        }
        else if (playedCard.subtype == "Projectile")
        {
            Projectile();
        }
        //check if the other person is still resolving an attack, if not, end interaction
        gameManager.EndInteraction();
    }

    public void Strike()
    {
        if (playedCard.range < distance || opponent.isDodging == true)
        {
            Debug.Log("Whiff!");
            opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onWhiff);
            opponent.plusFramesText.text = "+" + opponent.plusFrames;
        }
        if (playedCard.range >= distance && opponent.isDodging == false)
        {
            if (opponent.isBlockingHigh == true && (playedCard.guard == "High" || playedCard.guard == "Mid"))
            {
                if (dragonInstall > 0)
                {
                    Debug.Log("Blocked!");
                    if (playedCard.onBlock + 1 >= 0)
                    {
                        plusFrames = plusFrames + Mathf.Abs(playedCard.onBlock + 1);
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onBlock + 1);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                    }
                    energy = energy + playedCard.cost;
                    energyText.text = "" + energy;
                    return;
                }
                else
                {
                    Debug.Log("Blocked!");
                    if (playedCard.onBlock >= 0)
                    {
                        plusFrames = plusFrames + Mathf.Abs(playedCard.onBlock);
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onBlock);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                    }
                    energy = energy + playedCard.cost;
                    energyText.text = "" + energy;
                    return;
                }

            }
            if (opponent.isBlockingLow == true && (playedCard.guard == "Low" || playedCard.guard == "Mid"))
            {
                if (dragonInstall > 0)
                {
                    if (playedCard.onBlock + 1 >= 0)
                    {
                        plusFrames = plusFrames + Mathf.Abs(playedCard.onBlock + 1);
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onBlock + 1);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                    }
                    Debug.Log("Blocked!");
                    energy = energy + playedCard.cost;
                    energyText.text = "" + energy;
                    return;
                }
                else
                {
                    Debug.Log("Blocked!");
                    if (playedCard.onBlock >= 0)
                    {
                        plusFrames = plusFrames + Mathf.Abs(playedCard.onBlock);
                        plusFramesText.text = "+" + plusFrames;
                    }
                    else
                    {
                        opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onBlock);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                    }
                    energy = energy + playedCard.cost;
                    energyText.text = "" + energy;
                    return;
                }

            }
            if (character.cardName == "Zyla" && (opponent.isBlockingLow == true && playedCard.guard == "High") || (opponent.isBlockingHigh == true && playedCard.guard == "Low"))
            {
                if (dragonInstall > 0)
                {
                    Debug.Log("Get Mixed!");
                    opponent.health = opponent.health - (playedCard.damage + 2);
                    opponent.healthText.text = "" + opponent.health;
                    plusFrames = plusFrames + playedCard.onHit + 1;
                    plusFramesText.text = "+" + plusFrames;
                    isPushing = true;
                    Move(1);
                    opponent.isHit = true;
                    return;
                }
                else
                {
                    Debug.Log("Get Mixed!");
                    opponent.health = opponent.health - playedCard.damage;
                    opponent.healthText.text = "" + opponent.health;
                    plusFrames = plusFrames + playedCard.onHit + 1;
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
                opponent.health = opponent.health - (playedCard.damage + 2);
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + playedCard.onHit;
                plusFramesText.text = "+" + plusFrames;
                isPushing = true;
                Move(1);
                opponent.isHit = true;
            }
            else
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - playedCard.damage;
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + playedCard.onHit;
                plusFramesText.text = "+" + plusFrames;
                isPushing = true;
                Move(1);
                opponent.isHit = true;
            }

            if (playedCard.cardName == "Flash Kick")
            {
                opponent.Move(-3);
            }
        }

    }

    public void Throw()
    {
        if (playedCard.range < distance || opponent.isDodging == true)
        {
            Debug.Log("Whiff!");
            opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onWhiff);
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
                opponent.health = opponent.health - (playedCard.damage * 3);
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + playedCard.onHit;
                plusFramesText.text = "+" + plusFrames;
                empowered = false;
                opponent.isHit = true;
            }
            else
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - playedCard.damage;
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + playedCard.onHit;
                plusFramesText.text = "+" + plusFrames;
                opponent.isHit = true;
            }
            if (playedCard.cardName == "Grab")
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
            playedCard.range++;
        }
        if (playedCard.range < distance || opponent.isDodging == true)
        {
            Debug.Log("Whiff!");
            opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onWhiff);
            opponent.plusFramesText.text = "+" + opponent.plusFrames;
        }
        if (playedCard.range >= distance && opponent.isDodging == false)
        {
            if (opponent.isBlockingHigh == true && (playedCard.guard == "High" || playedCard.guard == "Mid"))
            {
                Debug.Log("Blocked!");
                opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onBlock);
                opponent.plusFramesText.text = "+" + opponent.plusFrames;
                return;
            }
            if (opponent.isBlockingLow == true && (playedCard.guard == "Low" || playedCard.guard == "Mid"))
            {
                Debug.Log("Blocked!");
                opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onBlock);
                opponent.plusFramesText.text = "+" + opponent.plusFrames;
                return;
            }
            if (forceBreak > 0)
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - (playedCard.damage + 1);
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + playedCard.onHit;
                plusFramesText.text = "+" + plusFrames;
                opponent.energy = opponent.energy - playedCard.cost;
                opponent.isHit = true;
            }
            else
            {
                Debug.Log("Hit!");
                opponent.health = opponent.health - playedCard.damage;
                opponent.healthText.text = "" + opponent.health;
                plusFrames = plusFrames + playedCard.onHit;
                plusFramesText.text = "+" + plusFrames;
                opponent.energy = opponent.energy - playedCard.cost;
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
