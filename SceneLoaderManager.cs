using UnityEngine;
using Unity.Netcode;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Asynchronously load the main scene
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        // Load the scene
        yield return new WaitUntil(() => NetworkManager.Singleton != null);
        SceneManager.LoadScene("MainMenu");
    }
}
