using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class Player : NetworkBehaviour
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
    public bool isDone = false;
    public bool isFree = false;
    public bool doingNothingCosts = false;
    public bool wereDone = false;
    public int cumulativePlusFrames, lastPosition, lastStagePosition;
    public float lastFlipCheck;

    public GameObject revealedCard;
    public DisplayCard revealedCardScript;
    public TMP_Text revealedCardCostText, opponentRevealedCardCostText;
    public TMP_Text waitingForOpponent;
    public GameObject installTextGameObject, healthTextGameObject, energyTextGameObject, plusFramesTextGameObject,
    revealedCardCostTextGameObject, opponentRevealedCardCostTextGameObject, abilityOneButtonGameObject, abilityTwoButtonGameObject, waitingGameObject,
    gameManagerObject, characterCardGameObject, canvas, characterSelectScreenGameObject, backgroundImage;
    public CharacterSelect characterSelectScreen;


    public DisplayCharacterCard characterCard;

    public NonBasicCard playedCard;

    public int health, energy, plusFrames, finalCardCost;
    public TMP_Text healthText, energyText, plusFramesText;

    public Button abilityOneButton, abilityTwoButton;

    public Image playerSprite, opponentSprite;

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
        backgroundImage = GameObject.FindGameObjectWithTag("Background");
        characterSelectScreenGameObject = GameObject.FindGameObjectWithTag("Character Select Screen");
        characterSelectScreen = characterSelectScreenGameObject.GetComponent<CharacterSelect>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.transform.SetParent(canvas.transform);
        this.transform.SetAsFirstSibling();
        transform.localScale = new Vector3(2, 3, 0);
        playerSprite = this.GetComponent<Image>();
        playerSprite.enabled = false;

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
            opponentRevealedCardCostTextGameObject = GameObject.FindGameObjectWithTag("P2 Revealed Card Cost");
            opponentRevealedCardCostText = opponentRevealedCardCostTextGameObject.GetComponent<TMP_Text>();
            abilityOneButtonGameObject = GameObject.FindGameObjectWithTag("P1 Ability 1");
            abilityTwoButtonGameObject = GameObject.FindGameObjectWithTag("P1 Ability 2");
            abilityOneButton = abilityOneButtonGameObject.GetComponent<Button>();
            abilityTwoButton = abilityTwoButtonGameObject.GetComponent<Button>();
            characterCardGameObject = GameObject.FindGameObjectWithTag("P1 Character");
            characterCard = characterCardGameObject.GetComponent<DisplayCharacterCard>();
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
            opponentRevealedCardCostTextGameObject = GameObject.FindGameObjectWithTag("P1 Revealed Card Cost");
            opponentRevealedCardCostText = opponentRevealedCardCostTextGameObject.GetComponent<TMP_Text>();
            abilityOneButtonGameObject = GameObject.FindGameObjectWithTag("P2 Ability 1");
            abilityTwoButtonGameObject = GameObject.FindGameObjectWithTag("P2 Ability 2");
            abilityOneButton = abilityOneButtonGameObject.GetComponent<Button>();
            abilityTwoButton = abilityTwoButtonGameObject.GetComponent<Button>();
            otherPlayerGameObject = GameObject.FindGameObjectWithTag("P1");
            opponentSprite = otherPlayerGameObject.GetComponent<Image>();
            otherPlayer = otherPlayerGameObject.GetComponent<Transform>();
            opponent = otherPlayerGameObject.GetComponent<Player>();
            characterCardGameObject = GameObject.FindGameObjectWithTag("P2 Character");
            characterCard = characterCardGameObject.GetComponent<DisplayCharacterCard>();
            characterCard.SetCharacter(2);
            gameManager.SetPlayer();
            opponent.SetOpponent();
            characterSelectScreen.SetOpponentCharacterSelect();
            position = 7;
            this.transform.position = stagePositions[6].transform.position;
            backgroundImage.transform.SetAsFirstSibling();
        }

        installText.text = "";
        healthText.text = "30";
        energyText.text = "5";
        plusFramesText.text = "";
    }

    public void SetOpponent()
    {
        otherPlayerGameObject = GameObject.FindGameObjectWithTag("P2");
        opponentSprite = otherPlayerGameObject.GetComponent<Image>();
        otherPlayer = otherPlayerGameObject.GetComponent<Transform>();
        opponent = otherPlayerGameObject.GetComponent<Player>();
        gameManager.SetPlayer();
        characterCard.SetCharacter(1);
        backgroundImage.transform.SetAsFirstSibling();
        characterSelectScreen.SetOpponentCharacterSelect();
        revealedCard.SetActive(false);
        opponent.revealedCard.SetActive(false);
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
                opponent.distance = Mathf.Abs(position - opponent.position);
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
                opponent.distance = Mathf.Abs(position - opponent.position);
            }
        }

    }

    IEnumerator WaitForOpponentCharacter()
    {
        waitingForOpponent.text = "Waiting for opponent...";
        while (opponent.character == null)
        {
            yield return null;
        }
        playerSprite.enabled = true;
        opponentSprite.enabled = true;
        waitingForOpponent.text = "";
        characterSelectScreenGameObject.SetActive(false);
    }

    IEnumerator WaitForOpponent()
    {
        waitingForOpponent.text = "Waiting for opponent...";
        playedCard = gameManager.card;
        while (opponent.playedCard == null)
        {
            yield return null;
        }
        waitingForOpponent.text = "";
        StartCoroutine("RevealCards");
    }


    IEnumerator RevealCards()
    {
        gameManager.cutscene = true;
        revealedCardScript.card = playedCard;
        revealedCardScript.UpdateCard();
        opponent.revealedCardScript.card = opponent.playedCard;
        opponent.revealedCardScript.UpdateCard();
        revealedCard.SetActive(true);
        opponent.revealedCard.SetActive(true);
        revealedCardCostText.text = "" + finalCardCost;
        opponentRevealedCardCostText.text = "" + opponent.finalCardCost;
        yield return new WaitForSeconds(1.5f);
        revealedCard.SetActive(false);
        opponent.revealedCard.SetActive(false);
        StartCoroutine("FrameDelay");
        opponent.StartCoroutine("FrameDelay");
    }
    
    IEnumerator FrameDelay()
    {
        yield return new WaitForSeconds(finalCardCost * 0.1f);
        Debug.Log("Going to play " + playedCard);
        PlayCard();
    }

    public void PlayCard()
    {
        if(wereDone == true)
        {
            wereDone = false;
            gameManager.EndInteraction();
            return;
        }
             
        plusFrames = 0;
        plusFramesText.text = "";
        Debug.Log("Cost: " + finalCardCost);
        
        if(isFree == true)
        {
            Debug.Log("Card Free");
        }
        //if you can afford the card, play it
        else if (finalCardCost <= energy)
        {
            energy = energy - finalCardCost;
            energyText.text = "" + energy;
            Debug.Log("" + playedCard.cardName);
        }
        //if you can't afford the card, you do nothing, which costs 1 energy if you have more energy than your opponent
        else if (finalCardCost > energy)
        {
            if (doingNothingCosts == true)
            {
                energy--;
                energyText.text = "" + energy;
            }
            Debug.Log("Do Nothing");
            isDone = true;
            if (opponent.isDone == true)
            {
                gameManager.EndInteraction();
            }
            return;
        }

        if ((finalCardCost == opponent.finalCardCost) && opponent.finalCardCost <= opponent.energy)
        {
            if (playedCard.cardName == "Dash Forward" && opponent.playedCard.cardName == "Dash Forward" && distance <= 2)
            {
                if(opponent.isFree == false)
                {
                    opponent.energy--;
                    opponent.energyText.text = "" + opponent.energy;
                }
                if(distance == 1)
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
                else if(distance == 2)
                {
                    if (flipCheck < 0)
                    {
                        transform.position = stagePositions[opponent.position - 1].transform.position;
                        position = opponent.position;

                        opponent.transform.position = opponent.stagePositions[position - 3].transform.position;
                        opponent.position = position - 2;
                    }
                    else
                    {
                        transform.position = stagePositions[opponent.position - 1].transform.position;
                        position = opponent.position;

                        opponent.transform.position = opponent.stagePositions[position + 1].transform.position;
                        opponent.position = position + 2;
                    }
                }
                opponent.wereDone = true;
                return;
            }
            else if (playedCard.cardName == "CHARGE!!!" && opponent.playedCard.cardName == "CHARGE!!!" && distance < 5)
            {
                Debug.Log("Bonk");
                if(distance == 3 || distance == 4)
                {
                    Move(1);
                    opponent.Move(1);
                }
                opponent.energy = opponent.energy - opponent.finalCardCost;
                opponent.energyText.text = "" + opponent.energy;
                opponent.wereDone = true;
                return;
            }
            else if (playedCard.cardName == "Warp" && opponent.playedCard.cardName == "Warp")
            {
                opponent.energy = opponent.energy - opponent.finalCardCost;
                opponent.energyText.text = "" + opponent.energy;

                lastPosition = position;
                lastStagePosition = position + 1;
                lastFlipCheck = flipCheck;

                if (opponent.position != 1 && opponent.position != 9)
                {
                    if (opponent.flipCheck < 0)
                    {
                        transform.position = stagePositions[opponent.position - 2].transform.position;
                        position = opponent.position - 1;
                        distance = Mathf.Abs(position - opponent.position);
                        opponent.distance = Mathf.Abs(position - opponent.position);
                        flipCheck = -1;
                    }
                    if (opponent.flipCheck > 0)
                    {
                        transform.position = stagePositions[opponent.position].transform.position;
                        position = opponent.position + 1;
                        distance = Mathf.Abs(position - opponent.position);
                        opponent.distance = Mathf.Abs(position - opponent.position);
                        flipCheck = 1;
                    }

                    //Move(-3);
                }

                Debug.Log("Trying to find where the problem is");
                
                if (lastPosition != 1 && lastPosition != 9)
                {
                    if (lastFlipCheck < 0)
                    {
                        opponent.transform.position = stagePositions[lastPosition - 2].transform.position;
                        opponent.position = lastPosition - 1;
                        distance = Mathf.Abs(position - opponent.position);
                        opponent.distance = Mathf.Abs(position - opponent.position);
                        flipCheck = -1;
                    }
                    if (lastFlipCheck > 0)
                    {
                        opponent.transform.position = stagePositions[lastPosition].transform.position;
                        opponent.position = lastPosition + 1;
                        distance = Mathf.Abs(position - opponent.position);
                        opponent.distance = Mathf.Abs(position - opponent.position);
                        flipCheck = 1;
                    }

                    //opponent.Move(-3);
                }

                opponent.wereDone = true;
                return;
            }
            else if (playedCard.subtype == "Strike" && opponent.playedCard.subtype == "Strike")
            {
                Debug.Log("Fixing Interaction...");
                opponent.wereDone = true;
                return;
            }
            else if (playedCard.subtype == "Throw" && opponent.playedCard.subtype == "Throw")
            {
                if(opponent.playedCard.cardName == "Tackle" && playedCard.cardName == "Tackle")
                {
                    if(distance > 2)
                    {
                        opponent.Move(1);
                        Move(1);
                    }
                    else if(distance == 2)
                    {
                        energy = energy + finalCardCost;
                        energyText.text = "" + energy;
                        Debug.Log("Clash");
                        opponent.wereDone = true;
                        return;
                    }
                }
                else if(opponent.playedCard.cardName == "Tackle" && playedCard.cardName != "Tackle")
                {
                    opponent.Move(1);
                }
                else if(opponent.playedCard.cardName != "Tackle" && playedCard.cardName == "Tackle")
                {
                    Move(1);
                }

                if (distance > 1)
                {
                    Debug.Log("Double Whiff");
                    opponent.energy = opponent.energy - opponent.finalCardCost;
                    opponent.energyText.text = "" + opponent.energy;
                    plusFrames = plusFrames + Mathf.Abs(opponent.playedCard.onWhiff);
                    opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onWhiff);
                    cumulativePlusFrames = plusFrames - opponent.plusFrames;
                    if (cumulativePlusFrames > 0)
                    {
                        plusFrames = cumulativePlusFrames;
                        plusFramesText.text = "+" + plusFrames;
                        opponent.plusFrames = 0;
                        opponent.plusFramesText.text = "";

                    }
                    else if (cumulativePlusFrames < 0)
                    {
                        opponent.plusFrames = Mathf.Abs(cumulativePlusFrames);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                        plusFrames = 0;
                        plusFramesText.text = "";
                    }
                    else if (cumulativePlusFrames == 0)
                    {
                        plusFrames = cumulativePlusFrames;
                        plusFramesText.text = "" + plusFrames;
                        opponent.plusFrames = 0;
                        opponent.plusFramesText.text = "" + opponent.plusFrames;
                    }
                    opponent.wereDone = true;
                    return;
                }
                else
                {
                    energy = energy + finalCardCost;
                    energyText.text = "" + energy;
                    Debug.Log("Clash");
                    opponent.wereDone = true;
                    return;
                }
            }
            else if ((playedCard.subtype == "Projectile" && opponent.playedCard.subtype == "Projectile") || (playedCard.subtype == "Projectile Throw" && opponent.playedCard.subtype == "Projectile Throw"))
            {
                if (distance > (playedCard.range + opponent.playedCard.range))
                {
                    Debug.Log("Double Whiff");
                    opponent.energy = opponent.energy - opponent.finalCardCost;
                    opponent.energyText.text = "" + opponent.energy;
                    plusFrames = plusFrames + Mathf.Abs(opponent.playedCard.onWhiff);
                    opponent.plusFrames = opponent.plusFrames + Mathf.Abs(playedCard.onWhiff);
                    cumulativePlusFrames = plusFrames - opponent.plusFrames;
                    if(cumulativePlusFrames > 0)
                    {
                        plusFrames = cumulativePlusFrames;
                        plusFramesText.text = "+" + plusFrames;
                        opponent.plusFrames = 0;
                        opponent.plusFramesText.text = "";

                    }
                    else if(cumulativePlusFrames < 0)
                    {
                        opponent.plusFrames = Mathf.Abs(cumulativePlusFrames);
                        opponent.plusFramesText.text = "+" + opponent.plusFrames;
                        plusFrames = 0;
                        plusFramesText.text = "";
                    }
                    else if(cumulativePlusFrames == 0)
                    {
                        plusFrames = cumulativePlusFrames;
                        plusFramesText.text = "" + plusFrames;
                        opponent.plusFrames = 0;
                        opponent.plusFramesText.text = "" + opponent.plusFrames;
                    }
                    opponent.wereDone = true;
                    return;
                }
                else
                {
                    energy = energy + finalCardCost;
                    energyText.text = "" + energy;
                    Debug.Log("Clash");
                    opponent.wereDone = true;
                    return;
                }
            }

        }

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
                    plusFrames = plusFrames + 4;
                    plusFramesText.text = "+" + plusFrames;
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
                    opponent.distance = Mathf.Abs(position - opponent.position);
                    flipCheck = -1;
                }
                if (opponent.flipCheck > 0)
                {
                    transform.position = stagePositions[opponent.position].transform.position;
                    position = opponent.position + 1;
                    distance = Mathf.Abs(position - opponent.position);
                    opponent.distance = Mathf.Abs(position - opponent.position);
                    flipCheck = 1;
                }

                Move(-3);
                if (opponent.isDodging == true)
                {
                        plusFrames = plusFrames + 3;
                        plusFramesText.text = "+" + plusFrames;
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
        isDone = true;
        if(opponent.isDone == true)
        {
            gameManager.EndInteraction();
        }
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
