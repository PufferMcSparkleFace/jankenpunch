using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnAround : MonoBehaviour
{
    public Transform otherPlayer;
    public float flipCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        flipCheck = this.transform.position.x - otherPlayer.position.x;
        if(flipCheck > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
