using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public Transform hand = null;
    public Transform playArea = null;
    public int handPosition;
    public bool isInHand = true;

    public void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand").transform;
        playArea = GameObject.FindGameObjectWithTag("Play Area").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        handPosition = transform.GetSiblingIndex();
        transform.SetParent(this.transform.parent.parent);
        isInHand = false;
        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        transform.localScale = new Vector3(2.5f, 3.5f, 0);
        StopCoroutine("Enlarge");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent.tag == "Cards" && playArea.childCount == 0)
        {
            transform.SetParent(playArea);
        }
        else
        {
            transform.SetParent(hand);
            transform.SetSiblingIndex(handPosition);
            isInHand = true;
        }
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isInHand == true)
        {
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.25f);
            StartCoroutine("Enlarge");
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        transform.localScale = new Vector3(2.5f, 3.5f, 0);
        StopCoroutine("Enlarge");
    }

    IEnumerator Enlarge()
    {
        yield return new WaitForSeconds(1);
        transform.localScale = new Vector3(5, 7, 0);
        gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.21f);
    }

}
