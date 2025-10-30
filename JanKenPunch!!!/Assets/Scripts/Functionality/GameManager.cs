using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;

public class GameManager : NetworkBehaviour
{
    public Transform playArea;
    public GameObject lockInButton;
    public GameObject playedCard, backButton;
    [SerializeField]
    public NonBasicCard card;
    public GameObject[] cardsInHand;

    public int turnTimer;
    public TMP_Text turnTimerText, victoryText, defeatText, drawText;

    public Transform hand = null;

    public Draw deck;

    public int timer;
    public int spacesBehindP1;
    public int spacesBehindP2;
    public TMP_Text timerText;
    public Player me, opponent;
    public GameObject meGameObject, opponentGameObject;
    public bool discarding;
    public bool cutscene = false;
    public DisplayCharacterCard p1Card, p2Card;

    public NetworkManager network;

    public CharacterSelect characterSelect;

    public List<NonBasicCard> cardDatabase = new List<NonBasicCard>();

    public GameObject joinLobbyPanel, gameOverButtons, discardingUI;

    public int postGame = 0;

    public AudioManager audioManager;


    public void Start()
    {
        timer = 10;

        hand = GameObject.FindGameObjectWithTag("Hand").transform;

        audioManager = GameObject.FindFirstObjectByType<AudioManager>();

    }

    public void SetPlayer()
    {
        if(network.IsHost == true)
        {
            meGameObject = GameObject.FindGameObjectWithTag("P1");
            me = meGameObject.GetComponent<Player>();
            opponentGameObject = GameObject.FindGameObjectWithTag("P2");
            opponent = opponentGameObject.GetComponent<Player>();
            characterSelect.myCharacterGameObject = GameObject.FindGameObjectWithTag("P1 Character");
            characterSelect.myCharacter = characterSelect.myCharacterGameObject.GetComponent<DisplayCharacterCard>();
            characterSelect.mySpriteGameObject = GameObject.FindGameObjectWithTag("P1");
            characterSelect.mySprite = characterSelect.mySpriteGameObject.GetComponent<Image>();
            characterSelect.myPlayer = characterSelect.mySpriteGameObject.GetComponent<Player>();
            me.pc = true;
        }
        else
        {
            meGameObject = GameObject.FindGameObjectWithTag("P2");
            me = meGameObject.GetComponent<Player>();
            opponentGameObject = GameObject.FindGameObjectWithTag("P1");
            opponent = opponentGameObject.GetComponent<Player>();
            characterSelect.myCharacterGameObject = GameObject.FindGameObjectWithTag("P2 Character");
            characterSelect.myCharacter = characterSelect.myCharacterGameObject.GetComponent<DisplayCharacterCard>();
            characterSelect.mySpriteGameObject = GameObject.FindGameObjectWithTag("P2");
            characterSelect.mySprite = characterSelect.mySpriteGameObject.GetComponent<Image>();
            characterSelect.myPlayer = characterSelect.mySpriteGameObject.GetComponent<Player>();
            me.pc = true;
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
            }

        if(me != null && opponent != null)
        {
            joinLobbyPanel.SetActive(false);
        }

        if(discarding == true)
        {
            discardingUI.SetActive(true);
        }
        else
        {
            discardingUI.SetActive(false);
        }

    }


    public void EndInteraction()
    {
        me.cumulativePlusFrames = me.plusFrames - opponent.plusFrames;
        if (me.cumulativePlusFrames > 0)
        {
            me.plusFrames = me.cumulativePlusFrames;
            me.plusFramesText.text = "+" + me.plusFrames;
            opponent.plusFrames = 0;
            opponent.plusFramesText.text = "";

        }
        else if (me.cumulativePlusFrames < 0)
        {
            opponent.plusFrames = Mathf.Abs(me.cumulativePlusFrames);
            opponent.plusFramesText.text = "+" + opponent.plusFrames;
            me.plusFrames = 0;
            me.plusFramesText.text = "";
        }
        else if (me.cumulativePlusFrames == 0)
        {
            me.plusFrames = me.cumulativePlusFrames;
            me.plusFramesText.text = "" + me.plusFrames;
            opponent.plusFrames = 0;
            opponent.plusFramesText.text = "" + opponent.plusFrames;
        }

        //return to neutral
        card = null; 
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
        me.isDone = false;
        opponent.isDone = false;
        Debug.Log("Return to Neutral");

        if(me.health <= 0)
        {
            Debug.Log("I lose");
            defeatText.gameObject.SetActive(true);
            gameOverButtons.SetActive(true);
            cutscene = true;
        }
        if(opponent.health <= 0)
        {
            Debug.Log("I win");
            victoryText.gameObject.SetActive(true);
            gameOverButtons.SetActive(true);
            cutscene = true;
        }


        //if both players run out of energy, move on to the next round
        if (me.energy == 0 && opponent.energy == 0 && me.plusFrames == 0 && opponent.plusFrames == 0)
        {
            timer--;
            timerText.text = "" + timer;
            //if the timer gets to 0, game over
            if (timer == 0)
            {
                if (me.health > opponent.health)
                {
                    Debug.Log("I win");
                    victoryText.gameObject.SetActive(true);
                }
                else if (me.health < opponent.health)
                {
                    Debug.Log("I lose");
                    defeatText.gameObject.SetActive(true);
                }
                else if (me.health == opponent.health)
                {
                    Debug.Log("Draw");
                    drawText.gameObject.SetActive(true);
                }
                cutscene = true;
                gameOverButtons.SetActive(true);
            }
            else
            {
                //if the timer's not 0, refill energy
                RefillEnergy(false);
            }
        }
    }

    public void RefillEnergy(bool plusFrames)
    {
        if (me.flipCheck < 0)
        {
            spacesBehindP1 = me.position - 1;
            spacesBehindP2 = 9 - opponent.position;
        }
        else
        {
            spacesBehindP1 = 9 - me.position;
            spacesBehindP2 = opponent.position - 1;
        }
        if (spacesBehindP1 > 4)
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
        if (plusFrames == true)
        {
            timer--;
            timerText.text = "" + timer;
        }
        //if the timer gets to 0, game over
        if (timer == 0)
        {
            if (me.health > opponent.health)
            {
                Debug.Log("I win");
                victoryText.gameObject.SetActive(true);
            }
            else if (me.health < opponent.health)
            {
                Debug.Log("I lose");
                defeatText.gameObject.SetActive(true);
            }
            else if (me.health == opponent.health)
            {
                Debug.Log("Draw");
                drawText.gameObject.SetActive(true);
            }
            gameOverButtons.SetActive(true);
            cutscene = true;
        }
    }

    public void RestartGame(bool characterSelect)
    {
        Debug.Log("Restarting");
        cutscene = false;
        victoryText.gameObject.SetActive(false);
        defeatText.gameObject.SetActive(false);
        drawText.gameObject.SetActive(false);
        gameOverButtons.SetActive(false);
        me.waitingForOpponent.text = "";
        me.empowered = false;
        me.forceBreak = 0;
        me.dragonInstall = 0;
        me.installText.text = "";
        me.health = 30;
        me.healthText.text = "30";
        me.energy = 5;
        me.energyText.text = "5";
        me.plusFrames = 0;
        me.plusFramesText.text = "";
        me.healthBar.ChangeHealth(30);
        timer = 10;
        timerText.text = "10";
        opponent.empowered = false;
        opponent.forceBreak = 0;
        opponent.dragonInstall = 0;
        opponent.installText.text = "";
        opponent.health = 30;
        opponent.healthText.text = "30";
        opponent.energy = 5;
        opponent.energyText.text = "5";
        opponent.plusFrames = 0;
        opponent.plusFramesText.text = "";
        opponent.healthBar.ChangeHealth(30);
        me.abilityOneButton.gameObject.SetActive(false);
        me.abilityTwoButton.gameObject.SetActive(false);
        if (meGameObject.tag == "P1")
        {
            me.position = 3;
            meGameObject.transform.position = me.stagePositions[2].transform.position;
            me.flipCheck = -1;
            opponent.position = 7;
            opponentGameObject.transform.position = opponent.stagePositions[6].transform.position;
            opponent.flipCheck = 1;
        }
        else
        {
            me.position = 7;
            meGameObject.transform.position = me.stagePositions[6].transform.position;
            me.flipCheck = 1;
            opponent.position = 3;
            opponentGameObject.transform.position = opponent.stagePositions[2].transform.position;
            opponent.flipCheck = -1;
        }
        me.distance = Mathf.Abs(me.position - opponent.position);
        opponent.distance = Mathf.Abs(me.position - opponent.position);
        cardsInHand[7].transform.SetAsFirstSibling();
        cardsInHand[6].transform.SetAsFirstSibling();
        cardsInHand[5].transform.SetAsFirstSibling();
        cardsInHand[4].transform.SetAsFirstSibling();
        cardsInHand[3].transform.SetAsFirstSibling();
        cardsInHand[2].transform.SetAsFirstSibling();
        cardsInHand[1].transform.SetAsFirstSibling();
        cardsInHand[0].transform.SetAsFirstSibling();
        if (characterSelect == true)
        {
            me.character = null;
            opponent.character = null;
            p1Card.character = null;
            p2Card.character = null;
            me.characterSelectScreenGameObject.SetActive(true);
            audioManager.StopPlaying("Fight (Unlooped)");
            audioManager.StopPlaying("Fight (Looped)");
            audioManager.StopAllCoroutines();
            audioManager.Play("Menu (Unlooped)");
            audioManager.StartCoroutine("PlayMenuTheme");
            deck.discardPile.Clear();
            Debug.Log("Going to character select");
        }
        else
        {
            deck.ShuffleUp();
            deck.DrawHand();
            Debug.Log("Restarting...");
        }
        postGame = 0;
    }

    public void Restart()
    {
        if(postGame == 1)
        {
            RestartGame(false);
        }
        else
        {
            postGame = 1;
            victoryText.gameObject.SetActive(false);
            defeatText.gameObject.SetActive(false);
            drawText.gameObject.SetActive(false);
            gameOverButtons.SetActive(false);
            me.waitingForOpponent.text = "Waiting for opponent...";
        }

        RestartRpc();
    }

    [Rpc(SendTo.NotMe)]
    public void RestartRpc()
    {
        if(postGame == 1)
        {
            RestartGame(false);
        }
        else
        {
            postGame = 1;
        }

    }

    public void GoToCharacterSelect()
    {
        if(postGame == 1 || postGame == 2)
        {
            RestartGame(true);
        }
        else
        {
            postGame = 2;
            victoryText.gameObject.SetActive(false);
            defeatText.gameObject.SetActive(false);
            drawText.gameObject.SetActive(false);
            gameOverButtons.SetActive(false);
            me.waitingForOpponent.text = "Waiting for opponent...";
        }
        GoToCharacterSelectRpc();
    }

    [Rpc(SendTo.NotMe)]
    public void GoToCharacterSelectRpc()
    {

        if (postGame == 1 || postGame == 2)
        {
            RestartGame(true);
        }
        else
        {
            postGame = 2;
        }

    }

    public void MainMenu()
    {
        //kick em out of the lobby, set game back to how it was at the start
        postGame = 0;
        audioManager.StopPlaying("Fight (Unlooped)");
        audioManager.StopPlaying("Fight (Looped)");
        audioManager.StopAllCoroutines();
        audioManager.Play("Menu (Unlooped)");
        audioManager.StartCoroutine("PlayMenuTheme");
        cutscene = false;
        SceneManager.LoadScene("Start Menu");
        SendOpponentToMainMenuRpc();
    }

    [Rpc(SendTo.NotMe)]
    public void SendOpponentToMainMenuRpc()
    {
        //kick em out of the lobby, set game back to how it was at the start
        postGame = 0;
        audioManager.StopPlaying("Fight (Unlooped)");
        audioManager.StopPlaying("Fight (Looped)");
        audioManager.StopAllCoroutines();
        audioManager.Play("Menu (Unlooped)");
        audioManager.StartCoroutine("PlayMenuTheme");
        cutscene = false;
        SceneManager.LoadScene("Start Menu");
    }

    public void LockIn()
    {
        card = playedCard.GetComponent<DisplayCard>().card;

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
            card = null;
            return;
        }

        me.finalCardCost = card.cost - me.plusFrames;
        if (me.finalCardCost < 0)
        {
            me.finalCardCost = 0;
        }
        if((card.type == "Basic Defense" || card.type == "Basic Movement") && (me.energy < opponent.energy || me.energy == 0))
        {
            me.isFree = true;
        }
        else
        {
            me.isFree = false;
        }
        if(me.finalCardCost > me.energy)
        {
            if((me.energy < opponent.energy || me.energy == 0))
            {
                me.doingNothingCosts = false;
            }
            else
            {
                me.doingNothingCosts = true;
            }
        }

        me.abilityOneButton.gameObject.SetActive(false);
        me.abilityTwoButton.gameObject.SetActive(false);

        SetOpponentCardRpc(card.id, me.finalCardCost, me.isFree, me.doingNothingCosts);
        me.StartCoroutine("WaitForOpponent");

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

    [Rpc(SendTo.NotMe)]
    public void SetOpponentCardRpc(int cardID, int finalCardCost, bool isFree, bool doingNothingCosts)
    {
        Debug.Log("" + cardID);
        opponent.playedCard = cardDatabase[cardID];
        opponent.finalCardCost = finalCardCost;
        opponent.isFree = isFree;
        opponent.doingNothingCosts = doingNothingCosts;
    }

    public void AbilityOne()
    {
        if (discarding == true)
        {
            return;
        }
        me.plusFrames--;
        me.plusFramesText.text = "+" + me.plusFrames;
        if (me.plusFrames == 0)
        {
            me.plusFramesText.text = "";
        }
        if (me.character.cardName == "Zyla")
        {
            Debug.Log("Zyla +1");
            me.Move(1);
        }
        if (me.character.cardName == "Taibo")
        {
            Debug.Log("Taibo +1");
            discarding = true;
        }
        if (me.character.cardName == "Rynox")
        {
            Debug.Log("Rynox +1");
            me.energy++;
            me.energyText.text = "" + me.energy;
        }
        if (me.plusFrames == 0 && me.energy == 0 && opponent.energy == 0 && opponent.plusFrames == 0)
        {
            RefillEnergy(true);
        }
        AbilityOneRpc();

    }

    [Rpc(SendTo.NotMe)]
    public void AbilityOneRpc()
    {
        if (discarding == true)
        {
            return;
        }
        opponent.plusFrames--;
        opponent.plusFramesText.text = "+" + opponent.plusFrames;
        if (opponent.plusFrames == 0)
        {
            opponent.plusFramesText.text = "";
        }
        if (opponent.character.cardName == "Zyla")
        {
            Debug.Log("Zyla +1");
            opponent.Move(1);
        }
        if (opponent.character.cardName == "Taibo")
        {
            Debug.Log("Taibo +1");
        }
        if (opponent.character.cardName == "Rynox")
        {
            Debug.Log("Rynox +1");
            opponent.energy++;
            opponent.energyText.text = "" + opponent.energy;
        }
        if (me.plusFrames == 0 && me.energy == 0 && opponent.energy == 0 && opponent.plusFrames == 0)
        {
            RefillEnergy(true);
        }
    }

    public void AbilityTwo()
    {
        me.plusFrames = me.plusFrames - 3;
        me.plusFramesText.text = "+" + me.plusFrames;
        if (me.plusFrames == 0)
        {
            me.plusFramesText.text = "";
        }

        if (me.character.cardName == "Zyla")
        {
            me.dragonInstall = 5;
            Debug.Log("Zyla +3");
        }
        if (me.character.cardName == "Taibo")
        {
            me.forceBreak = 5;
            Debug.Log("Taibo +3");
        }
        if (me.character.cardName == "Rynox")
        {
            me.empowered = true;
            me.empoweredGameObject.SetActive(true);
            Debug.Log("Rynox +3");
        }
        if(me.plusFrames == 0 && me.energy == 0 && opponent.energy == 0 && opponent.plusFrames == 0)
        {
            RefillEnergy(true);
        }
        AbilityTwoRpc();
    }

    [Rpc(SendTo.NotMe)]
    public void AbilityTwoRpc()
    {
        opponent.plusFrames = opponent.plusFrames - 3;
        opponent.plusFramesText.text = "+" + opponent.plusFrames;
        if (opponent.plusFrames == 0)
        {
            opponent.plusFramesText.text = "";
        }

        if (opponent.character.cardName == "Zyla")
        {
            opponent.dragonInstall = 5;
            Debug.Log("Zyla +3");
        }
        if (opponent.character.cardName == "Taibo")
        {
            opponent.forceBreak = 5;
            Debug.Log("Taibo +3");
        }
        if (opponent.character.cardName == "Rynox")
        {
            opponent.empowered = true;
            opponent.empoweredGameObject.SetActive(true);
            Debug.Log("Rynox +3");
        }
        if (me.plusFrames == 0 && me.energy == 0 && opponent.energy == 0 && opponent.plusFrames == 0)
        {
            RefillEnergy(true);
        }
    }

    public void Back()
    {
        if (network.IsHost == true)
        {
            Debug.Log("DELETE THE LOBBY KILL IT");
        }
        SceneManager.LoadScene("Start Menu");
    }


}
