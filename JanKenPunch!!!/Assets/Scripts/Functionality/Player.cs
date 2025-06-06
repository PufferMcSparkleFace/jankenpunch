using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform otherPlayer;
    public float flipCheck;
    public GameObject[] stagePositions;
    public int position;
    public bool isBlockingHigh, isBlockingLow, isDodging, isMoving, isPushing = false;
    public int unitsActual;
    public GameManager gameManager;
    public Player opponent;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "P1")
        {
            this.transform.position = stagePositions[6].transform.position;
            position = 7;
        }
        else
        {
            this.transform.position = stagePositions[2].transform.position;
            position = 3;
        }
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

    public void Move(int units)
    {
        Debug.Log("Move " + units.ToString() + " units");

        for (int i = 0; i < Mathf.Abs(units); i++)
        {
            //if you're facing right
            if (flipCheck < 0)
            {
                if (units > 0)
                {
                    unitsActual = 1;
                }
                else
                {
                    unitsActual = -1;
                }
                if (unitsActual > 0 && gameManager.distance == 1)
                {
                    return;
                }
                this.transform.position = stagePositions[(position - 1) + unitsActual].transform.position;
                position = position + unitsActual;
                gameManager.distance = Mathf.Abs(position - opponent.position);
            }
            //if you're facing left
            else
            {
                if(units > 0)
                {
                    unitsActual = -1;
                }
                else
                {
                    unitsActual = 1;
                }
                if (unitsActual < 0 && gameManager.distance == 1)
                {
                    return;
                }
                this.transform.position = stagePositions[(position - 1) + unitsActual].transform.position;
                position = position + unitsActual;
                gameManager.distance = Mathf.Abs(position - opponent.position);
            }
        }

    }
}
