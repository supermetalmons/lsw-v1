using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using TMPro;


public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;
    public string appID;
    public string appVersion;
    public TMP_Text chatDisplay;
    public TMP_InputField chatField;
    public string currentChat;
    public GameObject messagePrefab;
    public Transform chatHolder;
    public Color[] messageColors;

    // Flag to track if chat history has been fetched
    private bool chatHistoryFetched = false;

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("Debug level: " + level + ", Message: " + message);
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("Chat state changed: " + state);
    }

    public void OnConnected()
    {
        Debug.Log("Connected");
        // Always fetch history upon connecting. Removed the conditional check to ensure history is fetched.
        chatClient.Subscribe(new string[] { "GlobalChannel" }, 10); // 10 is the number of historical messages to fetch.
    }


    public void OnDisconnected()
    {

        // Attempt to reconnect after a short delay
        Debug.Log("Attempting to reconnect...");

        string nickname = PlayerPrefs.GetString("Nickname");
        chatClient.Connect(appID, appVersion, new AuthenticationValues(nickname));
    }

    private IEnumerator ReconnectAfterDelay()
    {
        // Introduce a delay before attempting to reconnect
        yield return new WaitForSeconds(2f); // Adjust the delay duration as needed

        // Check if the chat client is not already connected
        if (!chatClient.CanChat)
        {
            Debug.Log("Attempting to reconnect...");

            string nickname = PlayerPrefs.GetString("Nickname");
            chatClient.Connect(appID, appVersion, new AuthenticationValues(nickname));
        }
        else
        {
            Debug.Log("Chat client is already connected.");
        }
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            // Generate random color for the message
            Color randomColor = messageColors[Random.Range(0, messageColors.Length)];

            // Construct the message string
            string msgs = string.Format("<color=#{0}>{1}: {2}</color>", ColorUtility.ToHtmlStringRGB(randomColor), senders[i], messages[i]);

            // Instantiate a new message prefab
            GameObject newMessage = Instantiate(messagePrefab, chatHolder);

            // Get the TextMeshProUGUI component and set the message text
            TextMeshProUGUI textMeshPro = newMessage.GetComponent<TextMeshProUGUI>();
            textMeshPro.text = msgs;

            // Set the color of the text
            textMeshPro.color = randomColor;

            // Adjust the position of the new message to stack from bottom to top
            RectTransform newMessageRectTransform = newMessage.GetComponent<RectTransform>();
            Vector2 anchoredPosition = newMessageRectTransform.anchoredPosition;
            anchoredPosition.y = -chatHolder.childCount * newMessageRectTransform.rect.height;
            newMessageRectTransform.anchoredPosition = anchoredPosition;
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log("Received private message from " + sender + " in channel " + channelName + ": " + message);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log("Status update for user " + user + ": " + status + ", Got message: " + gotMessage + ", Message: " + message);
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed to channels: " + string.Join(", ", channels));

        // Set flag to indicate that chat history has been fetched
        chatHistoryFetched = true;
    }

    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log("Unsubscribed from channels: " + string.Join(", ", channels));
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log("User " + user + " subscribed to channel " + channel);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log("User " + user + " unsubscribed from channel " + channel);
    }

    void Start()
    {
        chatClient = new ChatClient(this);
        string nickname = PlayerPrefs.GetString("Nickname");
        chatClient.Connect(appID, appVersion, new AuthenticationValues(nickname));
    }

    void Update()
    {
        chatClient.Service();
    }

    public void GetMessageFromUser(string message)
    {
        currentChat = message;
    }

    public void SubmitChatOnClick()
    {
        if (!string.IsNullOrEmpty(currentChat))
        {
            chatClient.PublishMessage("GlobalChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
        else
        {
            Debug.Log("Cannot send an empty message.");
        }
    }
}
