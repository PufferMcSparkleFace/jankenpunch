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
    public Sprite zylaSprite, taiboSprite, rynoxSprite;
    public List<NonBasicCard> zylaDeck = new List<NonBasicCard>();
    public List<NonBasicCard> taiboDeck = new List<NonBasicCard>();
    public List<NonBasicCard> rynoxDeck = new List<NonBasicCard>();

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

    public void SelectZyla()
    {
        Debug.Log("Zyla");
        myCharacter.character = zyla;
        myCharacter.UpdateCharacter();
        deck.deck = zylaDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        mySprite.sprite = zylaSprite;
        SelectZylaRpc();
        myPlayer.StartCoroutine("WaitForOpponentCharacter");
    }

    public void SelectTaibo()
    {
        Debug.Log("Taibo");
        myCharacter.character = taibo;
        myCharacter.UpdateCharacter();
        deck.deck = taiboDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        mySprite.sprite = taiboSprite;
        SelectTaiboRpc();
        myPlayer.StartCoroutine("WaitForOpponentCharacter");
    }

    public void SelectRynox()
    {
        Debug.Log("Rynox");
        myCharacter.character = rynox;
        myCharacter.UpdateCharacter();
        deck.deck = rynoxDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        mySprite.sprite = rynoxSprite;
        SelectRynoxRpc();
        myPlayer.StartCoroutine("WaitForOpponentCharacter");
    }

    [Rpc(SendTo.NotMe)]
    public void SelectZylaRpc()
    {
        opponentCharacter.character = zyla;
        opponentCharacter.UpdateCharacter();
        opponentSprite.sprite = zylaSprite;
    }

    [Rpc(SendTo.NotMe)]
    public void SelectTaiboRpc()
    {
        opponentCharacter.character = taibo;
        opponentCharacter.UpdateCharacter();
        opponentSprite.sprite = taiboSprite;
    }

    [Rpc(SendTo.NotMe)]
    public void SelectRynoxRpc()
    {
        opponentCharacter.character = rynox;
        opponentCharacter.UpdateCharacter();
        opponentSprite.sprite = rynoxSprite;
    }
}
