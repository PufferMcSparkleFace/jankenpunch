using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DisplayCard : MonoBehaviour
{
    public NonBasicCard card;

    public TMP_Text nameText;
    public TMP_Text costText;
    public Image image;
    public Image cardBack;
    public TMP_Text typeText;
    public TMP_Text rangeText;
    public TMP_Text effectText;
    public TMP_Text damageText;
    public TMP_Text onHitText;
    public TMP_Text onBlockText;
    public TMP_Text onWhiffText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.cardName;
        costText.text = card.cost.ToString();
        image.sprite = card.image;
        cardBack.sprite = card.cardBack;
        effectText.text = card.effect;
        if(card.type != "Attack")
        {
            typeText.text = card.type;
            if(card.type == "Basic Defense" || card.type == "Basic Movement")
            {
                costText.text = " ";
            }
            rangeText.text = " ";
            damageText.text = " ";
            onHitText.text = " ";
            onBlockText.text = " ";
            onWhiffText.text = " ";
            return;
        }
        if (card.subtype == "Throw")
        {
            onBlockText.text = "*";
            typeText.text = card.type + " - " + card.subtype;
        }
        else
        {
            onBlockText.text = card.onBlock.ToString();
            typeText.text = card.type + " - " + card.subtype + " - " + card.guard;
        }
        rangeText.text = card.range.ToString();
        damageText.text = card.damage.ToString();
        onHitText.text = card.onHit.ToString();
        onWhiffText.text = card.onWhiff.ToString();
    }

}
