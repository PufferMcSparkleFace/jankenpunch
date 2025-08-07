using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public Transform hand = null;
    public Transform playArea = null;
    public bool isInHand = true;
    public GameManager gameManager;

    public void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand").transform;
        playArea = GameObject.FindGameObjectWithTag("Play Area").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameManager.cutscene == false)
        {
            transform.SetParent(this.transform.parent.parent);
            isInHand = false;
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            transform.localScale = new Vector3(2.5f, 3.5f, 0);
            StopCoroutine("Enlarge");
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameManager.cutscene == false)
        {
            transform.position = eventData.position;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameManager.cutscene == false)
        {
            if (transform.parent.tag == "Cards" && playArea.childCount == 0)
            {
                transform.SetParent(playArea);
            }
            else
            {
                transform.SetParent(hand);
            }
        }
        
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameManager.cutscene == false)
        {
            if (isInHand == true && gameManager.playedCard == null)
            {
                gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.25f);
                StartCoroutine("Enlarge");
            }
        }
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameManager.cutscene == false)
        {
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            transform.localScale = new Vector3(2.5f, 3.5f, 0);
            StopCoroutine("Enlarge");
        }
        
    }

    IEnumerator Enlarge()
    {
        if (gameManager.cutscene == false)
        {
            yield return new WaitForSeconds(0.5f);
            transform.localScale = new Vector3(5, 7, 0);
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, -0.4f);
        }
        
    }

    private void Update()
    {
        if (transform.parent.name == "Hand")
        {
            isInHand = true;
        }
    }

}
