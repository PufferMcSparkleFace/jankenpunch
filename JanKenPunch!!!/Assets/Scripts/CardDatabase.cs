using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<NonBasicCard> cardList = new List<NonBasicCard>();

    private void Awake()
    {
        //cardList.Add(new NonBasicCard(ID, "Name", Cost, "Type", "Subtype", Range, "Guard", "Effect", Damage, On Hit, On Block, On Whiff));
        cardList.Add(new NonBasicCard(1.0f, "Jab", 2, "Attack", "Strike", 1, "Mid", "", 1, 1, 0, -1));
        cardList.Add(new NonBasicCard(1.1f, "Crouching Medium Kick", 3, "Attack", "Strike", 2, "Low", "", 2, 2, -2, -2));
        cardList.Add(new NonBasicCard(1.2f, "Collarbone Breaker", 3, "Attack", "Strike", 1, "High", "", 3, 2, -2, -2));
        cardList.Add(new NonBasicCard(1.3f, "Grab", 3, "Attack", "Throw", 1, "", "Either push the target back 1 unit or swap spaces with them", 5, 1, 100, -3));
        cardList.Add(new NonBasicCard(1.4f, "Fireball", 4, "Attack", "Projectile", 4, "Mid", "Push the target back 1 unit", 2, 0, -2, -5));
        cardList.Add(new NonBasicCard(1.5f, "Dragon Uppercut", 2, "Attack", "Strike", 1, "Mid", "Push the target back 3 units", 3, 1, -5, -5));
        cardList.Add(new NonBasicCard(1.6f, "Hurricane Kick", 4, "Attack", "Strike", 1, "Mid", "Airborne, Move forward 2 units", 1, 3, -3, -4));
        cardList.Add(new NonBasicCard(1.7f, "Focus Attack", 5, "Attack", "Strike", 1, "Mid", "Armored, Raw: +3/+0/+0", 5, +3, -3, -5));
        cardList.Add(new NonBasicCard(1.8f, "Far Slash", 3, "Attack", "Strike", 1, "Mid", "", 3, 2, 1, -2));
        cardList.Add(new NonBasicCard(1.9f, "Standing Medium Kick", 3, "Attack", "Strike", 2, "Mid", "", 3, 2, -1, -3));
    }
}
