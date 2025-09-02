using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class NonBasicCard : ScriptableObject
{
    public int id;
    public string cardName;
    public int cost;
    public Sprite image;
    public Sprite cardBack;
    public string type;
    public string subtype;
    public int range;
    public string guard;
    public string effect;
    public int damage;
    public int onHit;
    public int onBlock;
    public int onWhiff;
}
