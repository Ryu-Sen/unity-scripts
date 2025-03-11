using UnityEngine;
using TMPro;
using Steamworks;
using Steamworks.Data;
using System;

public class ChatManager : MonoBehaviour
{

    // Remove the serialized field for the chat input field in production
    [SerializeField]
    private bool isChatActive = false;

    [SerializeField]
    private TMP_InputField chatInputField;


    [SerializeField]
    private TextMeshProUGUI chatText;

    [SerializeField]
    private GameObject chatPanel;

    private void Start()
    {
        // Empty the chat text
        chatText.text = "";
    }

    private void OnEnable()
    {
        SteamMatchmaking.OnChatMessage += OnChatMessage; // This is called for when you receive a chat message
    }

    private void OnChatMessage(Lobby lobby, Friend friend, string arg3)
    {
        AddMessageToChat(arg3);
    }

    private void AddMessageToChat(string arg3)
    {
        chatText.text += arg3 + "\n";
    }

    private void Update()
    {
        // TODO: clean this code up
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isChatActive = chatPanel.activeSelf;
            if (isChatActive)
            {
                LobbySaver.instance.currentLobby?.SendChatString(chatInputField.text);
                chatInputField.text = "";
                chatInputField.DeactivateInputField();
                chatPanel.SetActive(!isChatActive);
            }
            else
            {
                chatInputField.gameObject.SetActive(true);
                chatInputField.Select();
                chatInputField.ActivateInputField();
                chatPanel.SetActive(!isChatActive);

            }
        }

    }
}
