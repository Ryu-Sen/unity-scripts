using UnityEngine;
using Steamworks.Data;

public class LobbySaver : MonoBehaviour
{
    public Lobby? currentLobby;
    public static LobbySaver instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);        
    }

}
