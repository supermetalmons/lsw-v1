using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayFabManager playFabManager;
    void Start()
    {
        playFabManager.RetrieveCharacterSelection();
        int characterIndex = playFabManager.getCharacterIndex();
        gameObject.transform.GetChild(characterIndex).gameObject.SetActive(true);
    }
}
