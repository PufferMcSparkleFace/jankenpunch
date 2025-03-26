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
        rangeText.text = card.range.ToString();
        effectText.text = card.effect;
        damageText.text = card.damage.ToString();
        onHitText.text = card.onHit.ToString();
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
        onWhiffText.text = card.onWhiff.ToString();
    }

}
