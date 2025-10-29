using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : NetworkBehaviour
{
    public DisplayCharacterCard myCharacter, opponentCharacter;
    public GameObject myCharacterGameObject, mySpriteGameObject, opponentCharacterGameObject, opponentSpriteGameObject;
    public Image mySprite, opponentSprite;
    public Player myPlayer;
    public NetworkManager network;

    public Draw deck;

    public CharacterCards zyla, taibo, rynox;
    public Sprite zylaSprite, taiboSprite, rynoxSprite, zylaP2Sprite, taiboP2Sprite, rynoxP2Sprite;
    public List<NonBasicCard> zylaDeck = new List<NonBasicCard>();
    public List<NonBasicCard> taiboDeck = new List<NonBasicCard>();
    public List<NonBasicCard> rynoxDeck = new List<NonBasicCard>();

    public Image background;

    public List<Sprite> backgrounds = new List<Sprite>();

    public int stageSelected;

    public void SetOpponentCharacterSelect()
    {
        if(network.IsHost == true)
        {
            opponentCharacterGameObject = GameObject.FindGameObjectWithTag("P2 Character");
            opponentSpriteGameObject = GameObject.FindGameObjectWithTag("P2");
            opponentSprite = opponentSpriteGameObject.GetComponent<Image>();
            opponentCharacter = opponentCharacterGameObject.GetComponent<DisplayCharacterCard>();
        }
        else
        {
            opponentCharacterGameObject = GameObject.FindGameObjectWithTag("P1 Character");
            opponentSpriteGameObject = GameObject.FindGameObjectWithTag("P1");
            opponentSprite = opponentSpriteGameObject.GetComponent<Image>();
            opponentCharacter = opponentCharacterGameObject.GetComponent<DisplayCharacterCard>();
        }
    }

    public void SetStage()
    {
        stageSelected = UnityEngine.Random.Range(0, 6);
        background.sprite = backgrounds[stageSelected];
        SetStageRpc(stageSelected);
    }

    [Rpc(SendTo.NotMe)]
    public void SetStageRpc(int stage)
    {
        background.sprite = backgrounds[stage];
    }

    public void SelectZyla()
    {
        Debug.Log("Zyla");
        myCharacter.character = zyla;
        myCharacter.UpdateCharacter();
        deck.deck = zylaDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        SetStage();
        if(opponentCharacter.character ==  null || opponentCharacter.character.cardName == "Rynox" || opponentCharacter.character.cardName == "Taibo")
        {
            mySprite.sprite = zylaSprite;
            SelectZylaRpc(false);
            myPlayer.StartCoroutine("WaitForOpponentCharacter");
        }
        else if (opponentCharacter.character.cardName == "Zyla")
        {
            mySprite.sprite = zylaP2Sprite;
            SelectZylaRpc(true);
            myPlayer.StartCoroutine("WaitForOpponentCharacter");
        }
    }

    public void SelectTaibo()
    {
        Debug.Log("Taibo");
        myCharacter.character = taibo;
        myCharacter.UpdateCharacter();
        deck.deck = taiboDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        SetStage();
        if (opponentCharacter.character == null || opponentCharacter.character.cardName == "Rynox" || opponentCharacter.character.cardName == "Zyla")
        {
            mySprite.sprite = taiboSprite;
            SelectTaiboRpc(false);
            myPlayer.StartCoroutine("WaitForOpponentCharacter");
        }
        else if (opponentCharacter.character.cardName == "Taibo")
        {
            mySprite.sprite = taiboP2Sprite;
            SelectTaiboRpc(true);
            myPlayer.StartCoroutine("WaitForOpponentCharacter");
        }
    }

    public void SelectRynox()
    {
        Debug.Log("Rynox");
        myCharacter.character = rynox;
        myCharacter.UpdateCharacter();
        deck.deck = rynoxDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        SetStage();
        if (opponentCharacter.character == null || opponentCharacter.character.cardName == "Taibo" || opponentCharacter.character.cardName == "Zyla")
        {
            mySprite.sprite = rynoxSprite;
            SelectRynoxRpc(false);
            myPlayer.StartCoroutine("WaitForOpponentCharacter");
        }
        else if (opponentCharacter.character.cardName == "Rynox")
        {
            mySprite.sprite = rynoxP2Sprite;
            SelectRynoxRpc(true);
            myPlayer.StartCoroutine("WaitForOpponentCharacter");
        }
    }

    [Rpc(SendTo.NotMe)]
    public void SelectZylaRpc(bool isMirror)
    {
        opponentCharacter.character = zyla;
        opponentCharacter.UpdateCharacter();
        if (isMirror == true)
        {
            opponentSprite.sprite = zylaP2Sprite;
        }
        else
        {
            opponentSprite.sprite = zylaSprite;
        }
    }

    [Rpc(SendTo.NotMe)]
    public void SelectTaiboRpc(bool isMirror)
    {
        opponentCharacter.character = taibo;
        opponentCharacter.UpdateCharacter();
        if(isMirror == true)
        {
            opponentSprite.sprite = taiboP2Sprite;
        }
        else
        {
            opponentSprite.sprite = taiboSprite;
        }
    }

    [Rpc(SendTo.NotMe)]
    public void SelectRynoxRpc(bool isMirror)
    {
        opponentCharacter.character = rynox;
        opponentCharacter.UpdateCharacter();
        if (isMirror == true)
        {
            opponentSprite.sprite = rynoxP2Sprite;
        }
        else
        {
            opponentSprite.sprite = rynoxSprite;
        }
    }
}
