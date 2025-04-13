using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterController : MonoBehaviour
{
    public static bool isHost = false;
    bool isInitialized = false;
    public GameObject turtle;

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
                    Debug.Log(NetworkManager.Singleton.ConnectedHostname);
                    Instantiate(turtle);
                }
                else
                {
                    NetworkManager.Singleton.StartClient();
                }
                isInitialized = true;
            }
        }
    }
}
