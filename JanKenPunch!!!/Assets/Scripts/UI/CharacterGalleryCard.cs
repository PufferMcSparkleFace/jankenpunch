using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterGalleryCard : MonoBehaviour
{
    public CharacterCards character;

    public TMP_Text nameText;
    public TMP_Text passiveText;
    public TMP_Text firstAbilityText;
    public TMP_Text secondAbilityCostText;
    public TMP_Text secondAbilityText;

    public Image image;
    public Image cardBack;

    // Start is called before the first frame update
    void Start()
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
