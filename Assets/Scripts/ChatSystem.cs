using System.Collections;
using System.Collections.Generic;
using Avro;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    public TMP_InputField userInputText;

    public GameObject chatPrefab;

    public Transform chatContainer;

    public void InstantiateMessage(Message message)
    {
        var newMessage = Instantiate(chatPrefab, transform.position, quaternion.identity);
        newMessage.transform.SetParent(chatContainer);
        newMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message.sender + ": ";
        newMessage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = message.text;
    }

    public void SendMessage()
    {
        name = PlayerPrefs.GetString("Nickname");
        //method to save the message to db
        userInputText.text = "";
    }
}
