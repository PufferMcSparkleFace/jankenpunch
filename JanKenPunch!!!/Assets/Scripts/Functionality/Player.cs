using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public CharacterCards character;
    public TMP_Text installText;
    public int dragonInstall = 0;
    public int forceBreak = 0;
    public bool empowered = false;

    public int health, energy, plusFrames;
    public TMP_Text healthText, energyText, plusFramesText;

    public Button abilityOneButton, abilityTwoButton;

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
        installText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        flipCheck = this.transform.position.x - otherPlayer.position.x;
        if(flipCheck > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (plusFrames == 0)
        {
            abilityOneButton.gameObject.SetActive(false);
            abilityTwoButton.gameObject.SetActive(false);
        }
        if (plusFrames >= 1)
        {
            abilityOneButton.gameObject.SetActive(true);
        }
        if (plusFrames >= 3)
        {
            abilityTwoButton.gameObject.SetActive(true);
        }
        if (gameManager.discarding == true)
        {
            abilityOneButton.gameObject.SetActive(false);
        }
        if (empowered == true || dragonInstall > 0 || forceBreak > 0)
        {
            abilityTwoButton.gameObject.SetActive(false);
        }
        if (empowered == false && dragonInstall <= 0 && forceBreak <= 0)
        {
            installText.text = "";
        }
        if(dragonInstall > 0)
        {
            installText.text = "Dragon Install: " + dragonInstall;
        }
        if (forceBreak > 0)
        {
            installText.text = "Force Break: " + forceBreak;
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
                    if(isPushing == false)
                    {
                        return;
                    }
                    else
                    {
                        if (opponent.position == 9 || opponent.position == 1)
                        {
                            return;
                        }
                        opponent.transform.position = stagePositions[opponent.position].transform.position;
                        opponent.position++;
                    }

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
                    if (isPushing == false)
                    {
                        return;
                    }
                    else
                    {
                        if(opponent.position == 9 || opponent.position == 1)
                        {
                            return;
                        }
                        opponent.transform.position = stagePositions[opponent.position -2].transform.position;
                        opponent.position--;
                    }
                }
                this.transform.position = stagePositions[(position - 1) + unitsActual].transform.position;
                position = position + unitsActual;
                gameManager.distance = Mathf.Abs(position - opponent.position);
            }
        }

    }

    public void AbilityOne()
    {
        if(gameManager.discarding == true)
        {
            return;
        }
        plusFrames--;
        plusFramesText.text = "+" + plusFrames;
        if(plusFrames == 0)
        {
            plusFramesText.text = "";
        }
        if(character.cardName == "Zyla")
        {
            Debug.Log("Zyla +1");
            Move(1);
        }
        if (character.cardName == "Taibo")
        {
            Debug.Log("Taibo +1");
            gameManager.discarding = true;
        }
        if (character.cardName == "Rynox")
        {
            Debug.Log("Rynox +1");
            energy++;
            energyText.text = "" + energy;
        }
    }

    public void AbilityTwo()
    {
        plusFrames = plusFrames -3;
        plusFramesText.text = "+" + plusFrames;
        if (plusFrames == 0)
        {
            plusFramesText.text = "";
        }

        if (character.cardName == "Zyla")
        {
            dragonInstall = 5;
            Debug.Log("Zyla +3");
        }
        if (character.cardName == "Taibo")
        {
            forceBreak = 5;
            Debug.Log("Taibo +3");
        }
        if (character.cardName == "Rynox")
        {
            empowered = true;
            installText.text = "Empowered";
            Debug.Log("Rynox +3");
        }
    }
}
