using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class BasicCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public int cost;
    public string type;
    public string effect;

    public BasicCard()
    {

    }

    public BasicCard(int Id, string CardName, int Cost, string Type, string Effect)
    {
        id = Id;
        cardName = CardName;
        cost = Cost;
        type = Type;
        effect = Effect;
    }
}
