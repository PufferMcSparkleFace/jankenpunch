using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public DisplayCharacterCard p1Character;
    public Image p1Sprite;

    public Draw deck;

    public CharacterCards zyla, taibo, rynox;
    public Sprite zylaSprite, taiboSprite, rynoxSprite;
    public List<NonBasicCard> zylaDeck = new List<NonBasicCard>();
    public List<NonBasicCard> taiboDeck = new List<NonBasicCard>();
    public List<NonBasicCard> rynoxDeck = new List<NonBasicCard>();

    public void SelectZyla()
    {
        Debug.Log("Zyla");
        p1Character.character = zyla;
        p1Character.UpdateCharacter();
        deck.deck = zylaDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        p1Sprite.sprite = zylaSprite;
        this.gameObject.SetActive(false);
    }

    public void SelectTaibo()
    {
        Debug.Log("Taibo");
        p1Character.character = taibo;
        p1Character.UpdateCharacter();
        deck.deck = taiboDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        p1Sprite.sprite = taiboSprite;
        this.gameObject.SetActive(false);
    }

    public void SelectRynox()
    {
        Debug.Log("Rynox");
        p1Character.character = rynox;
        p1Character.UpdateCharacter();
        deck.deck = rynoxDeck;
        deck.DrawHand();
        deck.characterSelected = true;
        p1Sprite.sprite = rynoxSprite;
        this.gameObject.SetActive(false);
    }
}
