using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public DisplayCard card6, card7, card8;

    public ScriptableObject[] zylaDeck = new ScriptableObject[30];
    public ScriptableObject[] discardPile = new ScriptableObject[30];

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DrawCard()
    {
        Debug.Log("Draw a card");
    }

}
