using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayCard : MonoBehaviour
{
    public List<NonBasicCard> displayCard = new List<NonBasicCard>();
    public int displayID;

    public float id;
    public string cardName;
    public int cost;
    public string type;
    public string subtype;
    public int range;
    public string guard;
    public string effect;
    public int damage;
    public int onHit;
    public int onBlock;
    public int onWhiff;

    public Text nameText;
    public Text costText;
    public Text typeText;
    public Text rangeText;
    public Text effectText;
    public Text damageText;
    public Text onHitText;
    public Text onBlockText;
    public Text onWhiffText;

    // Start is called before the first frame update
    void Start()
    {
        displayCard[0] = CardDatabase.cardList[displayID];
    }

    // Update is called once per frame
    void Update()
    {
        id = displayCard[0].id;
        cardName = displayCard[0].cardName;
        cost = displayCard[0].cost;
        type = displayCard[0].type;
        subtype = displayCard[0].subtype;
        range = displayCard[0].range;
        guard = displayCard[0].guard;
        effect = displayCard[0].effect;
        damage = displayCard[0].damage;
        onHit = displayCard[0].onHit;
        onBlock = displayCard[0].onBlock;
        onWhiff = displayCard[0].onWhiff;

        nameText.text = " " + cardName;
        costText.text = " " + cost;
        typeText.text = " " + type + subtype + guard;
        rangeText.text = " " + range;
        effectText.text = " " + effect;
        damageText.text = " " + damage;
        onHitText.text = " " + onHit;
        onBlockText.text = " " + onBlock;
        onWhiffText.text = " " + onWhiff;
    }
}
