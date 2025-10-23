using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterGallerySprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject blurb;

    public void OnPointerEnter(PointerEventData eventData)
    {
        blurb.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        blurb.SetActive(false);
    }

}
