using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : NetworkBehaviour
{
    public DisplayCharacterCard myCharacter;
    public GameObject myCharacterGameObject, mySpriteGameObject, oppponentCharacterGameObject, opponentSpriteGameObject;
    public Image mySprite, opponentSprite;
    public Player myPlayer;

    public Draw deck;

    public CharacterCards zyla, taibo, rynox;
    public Sprite zylaSprite, taiboSprite, rynoxSprite;
    public List<NonBasicCard> zylaDeck = new List<NonBasicCard>();
    public List<NonBasicCard> taiboDeck = new List<NonBasicCard>();
    public List<NonBasicCard> rynoxDeck = new List<NonBasicCard>();

    public void SelectZyla()
    {
        Debug.Log("Zyla");
        myCharacter.character = zyla;
        myCharacter.UpdateCharacter();
        deck.deck = zylaDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        mySprite.sprite = zylaSprite;
        SelectZylaRPC();
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
        myPlayer.StartCoroutine("WaitForOpponentCharacter");
    }

    [Rpc(SendTo.NotMe)]
    public void SelectZylaRPC()
    {
        myCharacter.character = zyla;
        myCharacter.UpdateCharacter();
        deck.characterSelected = true;
        mySprite.sprite = zylaSprite;
    }
}
