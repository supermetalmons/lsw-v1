using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TMP_InputField chatField;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {

    }

    public void EnterWorld()
    {
        CheckIfUserUpdatedNickname();
        SceneManager.LoadScene(1);
    }

    public void CheckIfUserUpdatedNickname()
    {
        if (PlayFabManager.instance.nicknameInputField.text != null)
        {
            PlayFabManager.instance.GetNicknameFromUser();
        }
    }
}
