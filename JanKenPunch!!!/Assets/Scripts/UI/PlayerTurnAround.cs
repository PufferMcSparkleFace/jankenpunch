using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnAround : MonoBehaviour
{
    public Transform otherPlayer;
    public float flipCheck;
    public GameObject[] stagePositions;
    public int position;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "P1")
        {
            this.transform.position = stagePositions[2].transform.position;
            position = 3;
        }
        else
        {
            this.transform.position = stagePositions[6].transform.position;
            position = 7;
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
}
