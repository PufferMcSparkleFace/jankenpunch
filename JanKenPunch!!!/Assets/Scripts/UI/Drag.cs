using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public Transform hand = null;
    public Transform playArea = null;

    public void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand").transform;
        playArea = GameObject.FindGameObjectWithTag("Play Area").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(this.transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.transform.parent.tag == "Cards" && playArea.childCount == 0)
        {
            this.transform.SetParent(playArea);
        }
        else
        {
            this.transform.SetParent(hand);
        }
            
    }

}
