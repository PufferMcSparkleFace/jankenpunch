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

    public GameManager gameManager;

    /*if you're player 1
        {
            myCharacterGameObject = GameObject.FindGameObjectWithTag("P1 Character");
            myCharacter = myCharacterGameObject.GetComponent<DisplayCharacterCard>();
            mySpriteGameObject = GameObject.FindGameObjectWithTag("P1");
            mySprite = mySpriteGameObject.GetComponent<Image>();
        }
        else
        {
            myCharacterGameObject = GameObject.FindGameObjectWithTag("P2 Character");
            myCharacter = myCharacterGameObject.GetComponent<DisplayCharacterCard>();
            mySpriteGameObject = GameObject.FindGameObjectWithTag("P2");
            mySprite = mySpriteGameObject.GetComponent<Image>();
        }*/

    public void SelectZyla()
    {
        Debug.Log("Zyla");
        myCharacter.character = zyla;
        myCharacter.UpdateCharacter();
        deck.deck = zylaDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        mySprite.sprite = zylaSprite;
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
        this.gameObject.SetActive(false);
    }
}
