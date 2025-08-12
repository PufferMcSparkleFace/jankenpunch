using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DisplayCharacterCard : MonoBehaviour
{
    public CharacterCards character;

    public TMP_Text nameText;
    public TMP_Text passiveText;
    public TMP_Text firstAbilityText;
    public TMP_Text secondAbilityCostText;
    public TMP_Text secondAbilityText;

    public Image image;
    public Image cardBack;

    public Player player;
    public GameObject playerGameObject;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = character.cardName;
        passiveText.text = character.passiveAbility;
        firstAbilityText.text = character.firstAbility;
        secondAbilityCostText.text = "+" + character.secondAbilityCost.ToString();
        secondAbilityText.text = character.secondAbility;
        image.sprite = character.image;
        cardBack.sprite = character.cardBack;

    }

    public void UpdateCharacter()
    {
        nameText.text = character.cardName;
        passiveText.text = character.passiveAbility;
        firstAbilityText.text = character.firstAbility;
        secondAbilityCostText.text = "+" + character.secondAbilityCost.ToString();
        secondAbilityText.text = character.secondAbility;
        image.sprite = character.image;
        cardBack.sprite = character.cardBack;
        player.character = character;
    }

    public void SetCharacter(int playernumber)
    {
        if (playernumber == 1)
        {
            playerGameObject = GameObject.FindGameObjectWithTag("P1");
            player = playerGameObject.GetComponent<Player>();
        }
        else
        {
            playerGameObject = GameObject.FindGameObjectWithTag("P2");
            player = playerGameObject.GetComponent<Player>();
        }
    }

}
