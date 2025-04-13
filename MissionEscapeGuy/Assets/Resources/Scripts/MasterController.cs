using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterController : MonoBehaviour
{
    public static bool isHost = false;
    bool isInitialized = false;
    public GameObject turtle;
    public static string connectTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") 
        {
            isInitialized = false;
        }
        else 
        {
            if (!isInitialized)
            {
                if (isHost)
                {
                    NetworkManager.Singleton.StartHost();
                }
                else
                {
                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(connectTo, 27586);
                    NetworkManager.Singleton.StartClient();
                }
                isInitialized = true;
            }

            if (isHost && SceneManager.GetActiveScene().name == "Lobby")
            {
                bool allReady = true;
                foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
                {
                    var playerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid);
                    var currentPlayer = playerObject.GetComponent<TheGuy>();
                    if (!currentPlayer.isReady.Value)
                    {
                        allReady = false;
                    }
                }
                if (allReady)
                {
                    SceneManager.LoadScene("GameWorld");
                }
            }
        }
    }
}
