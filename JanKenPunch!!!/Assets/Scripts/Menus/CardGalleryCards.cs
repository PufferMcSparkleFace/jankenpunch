using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CardGalleryCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public NonBasicCard card;
    public GameObject displayCard;
    public DisplayCard displayCardScript;

    public void OnPointerEnter(PointerEventData eventData)
    {
        displayCard.SetActive(true);
        displayCardScript.card = card;
        displayCardScript.UpdateCard();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        displayCard.SetActive(false);
    }

}
