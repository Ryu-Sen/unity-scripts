using UnityEngine;
using Steamworks;
using TMPro;
using Steamworks.Data;

public class SteamManager : MonoBehaviour
{

    public int maxPlayers = 6;

    [SerializeField]
    private TMP_InputField LobbyIDInputField;

    [SerializeField]
    private TextMeshProUGUI LobbyID;

    [SerializeField]
    private GameObject MainMenu;

    [SerializeField]
    private GameObject InLobbyMenu;


    void OnEnable()
    {
        SteamMatchmaking.OnLobbyCreated += LobbyCreated;
        SteamMatchmaking.OnLobbyEntered += LobbyEntered;
        SteamFriends.OnGameLobbyJoinRequested += GameLobbyJoinRequested;
    }
    void OnDisable()
    {
        SteamMatchmaking.OnLobbyCreated -= LobbyCreated;
        SteamMatchmaking.OnLobbyEntered -= LobbyEntered;
        SteamFriends.OnGameLobbyJoinRequested -= GameLobbyJoinRequested;
    }

    private async void GameLobbyJoinRequested(Lobby lobby, SteamId id)
    {
        await lobby.Join();
    }

    private void LobbyEntered(Lobby lobby)
    {
        LobbySaver.instance.currentLobby = lobby;
        LobbyID.text = lobby.Id.ToString();
        CheckUI();
    }

    private void LobbyCreated(Result result, Lobby lobby)
    {
        if (result == Result.OK)
        {
            lobby.SetPublic();
            lobby.SetJoinable(true);
        }
        else
        {
            Debug.LogError("Failed to create lobby");
        }
    }


    public async void HostLobby()
    {
        await SteamMatchmaking.CreateLobbyAsync(maxPlayers);
    }

    public async void JoinLobbyWithID()
    {
        ulong lobbyID = ulong.Parse(LobbyIDInputField.text);
        await SteamMatchmaking.JoinLobbyAsync(lobbyID);
    }

    public void CopyLobbyID()
    {
        GUIUtility.systemCopyBuffer = LobbyID.text;
    }

    public void LeaveLobby()
    {
        LobbySaver.instance.currentLobby?.Leave();
        LobbySaver.instance.currentLobby = null;
        CheckUI();
    }

    private void CheckUI()
    {
        if (LobbySaver.instance.currentLobby == null)
        {
            MainMenu.SetActive(true);
            InLobbyMenu.SetActive(false);
        }
        else
        {
            MainMenu.SetActive(false);
            InLobbyMenu.SetActive(true);
        }
    }
}
