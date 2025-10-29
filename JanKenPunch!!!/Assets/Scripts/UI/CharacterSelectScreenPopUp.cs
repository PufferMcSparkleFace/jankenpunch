using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CharacterSelectScreenPopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject character, card;

    public void OnPointerEnter(PointerEventData eventData)
    {
        character.SetActive(false);
        card.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        character.SetActive(true);
        card.SetActive(false);
    }
}
