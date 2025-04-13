using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterController : MonoBehaviour
{
    public static bool isHost = false;
    bool isInitialized = false;
    public TortleGuy turtle;
    public string connectTo;

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
                    Instantiate<TortleGuy>(turtle);
                }
                else
                {
                    connectTo = "152.10.97.41";
                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(connectTo, 27586);
                    NetworkManager.Singleton.StartClient();
                }
                isInitialized = true;
            }
        }
    }
}
