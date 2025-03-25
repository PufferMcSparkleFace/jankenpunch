using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class NonBasicCard : MonoBehaviour
{
    public int id;
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

    public NonBasicCard()
    {

    }

    public NonBasicCard(int Id, string CardName, int Cost, string Type, string Subtype, int Range, string Guard, string Effect, int Damage, int OnHit, int OnBlock, int OnWhiff)
    {
        id = Id;
        cardName = CardName;
        cost = Cost;
        type = Type;
        subtype = Subtype;
        range = Range;
        guard = Guard;
        effect = Effect;
        damage = Damage;
        onHit = OnHit;
        onBlock = OnBlock;
        onWhiff = OnWhiff;
    }
}
