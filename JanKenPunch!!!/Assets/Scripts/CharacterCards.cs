using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterCards : ScriptableObject
{
    public Sprite cardBack;
    public string cardName;
    public Sprite image;
    public string passiveAbility;
    public string firstAbility;
    public int secondAbilityCost;
    public string secondAbility;
}
