using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public DisplayCharacterCard p1Character;
    public CharacterCards zyla, taibo, rynox;

    public void SelectZyla()
    {
        Debug.Log("Zyla");
        p1Character.character = zyla;
        p1Character.UpdateCharacter();
        this.gameObject.SetActive(false);
    }

    public void SelectTaibo()
    {
        Debug.Log("Taibo");
        p1Character.character = taibo;
        p1Character.UpdateCharacter();
        this.gameObject.SetActive(false);
    }

    public void SelectRynox()
    {
        Debug.Log("Rynox");
        p1Character.character = rynox;
        p1Character.UpdateCharacter();
        this.gameObject.SetActive(false);
    }
}
