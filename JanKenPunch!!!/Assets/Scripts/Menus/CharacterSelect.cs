using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public DisplayCharacterCard myCharacter;
    public GameObject myCharacterGameObject, mySpriteGameObject;
    public Image mySprite;

    public Draw deck;

    public CharacterCards zyla, taibo, rynox;
    public Sprite zylaSprite, taiboSprite, rynoxSprite;
    public List<NonBasicCard> zylaDeck = new List<NonBasicCard>();
    public List<NonBasicCard> taiboDeck = new List<NonBasicCard>();
    public List<NonBasicCard> rynoxDeck = new List<NonBasicCard>();

    public void SelectCharacter(int character)
    {

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
        mySprite.enabled = true;
        this.gameObject.SetActive(false);
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
        mySprite.enabled = true;
        this.gameObject.SetActive(false);
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
        mySprite.enabled = true;
        this.gameObject.SetActive(false);
    }
}
