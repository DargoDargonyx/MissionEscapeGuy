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
                    var toSpawn = Instantiate(turtle);
                    toSpawn.GetComponent<NetworkObject>().Spawn();
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
