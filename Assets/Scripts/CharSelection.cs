using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelection : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public static CharSelection instance;
    public int characterSelectionIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        
    }

    public void nextCharacter()
    {
        characterPrefabs[characterSelectionIndex].SetActive(false);
        characterSelectionIndex = (characterSelectionIndex + 1) % characterPrefabs.Length;
        characterPrefabs[characterSelectionIndex].SetActive(true);
        PlayFabManager.instance.SaveCharacterSelection(characterSelectionIndex);
        PlayerPrefs.SetInt("characterIndex", characterSelectionIndex);

    }

    public void backCharacter()
    {
        characterPrefabs[characterSelectionIndex].SetActive(false);
        characterSelectionIndex--;
        if(characterSelectionIndex < 0)
        {
            characterSelectionIndex += characterPrefabs.Length;
        }
        characterPrefabs[characterSelectionIndex].SetActive(true);
        PlayFabManager.instance.SaveCharacterSelection(characterSelectionIndex);
        PlayerPrefs.SetInt("characterIndex", characterSelectionIndex);
    }

    public void SetActiveCharacter()
    {
        foreach (var characterPrefab in characterPrefabs)
        {
            characterPrefab.SetActive(false);
        }
        characterPrefabs[characterSelectionIndex].SetActive(true);
    }
}
