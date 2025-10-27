using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DisplayCharacterCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public GameObject enlargedCard;
    public DisplayCharacterCard enlargedCardScript;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "Untagged")
        {
            nameText.text = character.cardName;
            passiveText.text = character.passiveAbility;
            firstAbilityText.text = character.firstAbility;
            secondAbilityCostText.text = "-" + character.secondAbilityCost.ToString();
            secondAbilityText.text = character.secondAbility;
            image.sprite = character.image;
            cardBack.sprite = character.cardBack;
        }

    }

    public void UpdateCharacter()
    {
        nameText.text = character.cardName;
        passiveText.text = character.passiveAbility;
        firstAbilityText.text = character.firstAbility;
        secondAbilityCostText.text = "-" + character.secondAbilityCost.ToString();
        secondAbilityText.text = character.secondAbility;
        image.sprite = character.image;
        cardBack.sprite = character.cardBack;
        if(this.gameObject.tag != "Untagged")
        {
            player.character = character;
        }
    }

    public void SetCharacter(int playerNumber)
    {
        if (playerNumber == 1)
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


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameManager.cutscene == false)
        {
           StartCoroutine("Enlarge");
        }


    }

    public void OnPointerExit(PointerEventData eventData)
    {
       StopCoroutine("Enlarge");
       enlargedCard.SetActive(false);
    }

    IEnumerator Enlarge()
    {
        if (gameManager.cutscene == false)
        {
            yield return new WaitForSeconds(0.5f);     
            enlargedCardScript.character = character;
            enlargedCardScript.UpdateCharacter();
            enlargedCard.SetActive(true);
        }

    }

}
