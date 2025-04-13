using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class ClientManager : MonoBehaviour
{
    NetworkObject player;
    private float moveX;
    private float moveY;
    Quaternion targetRotation;
    public Camera camera;
    public static int selCol = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.Singleton.IsClient) 
        {
            player = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();

            if (SceneManager.GetActiveScene().name == "Lobby") 
            {
                string user = FindAnyObjectByType<TMP_InputField>().text;
                player.GetComponent<TheGuy>().playerCosmeticRpc(user, selCol);
            }

            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");

            Vector3 newCameraPos = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
            camera.transform.position = newCameraPos;

            checkDirection();
            SubmitNewPosition();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<TheGuy>();
                player.fireRpc();
            }
        }
    }

    private void checkDirection()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - (Vector2) player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }


    void SubmitNewPosition()
    {
        if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
        {
            foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid);
                var player = playerObject.GetComponent<TheGuy>();
                player.Move(moveX, moveY, targetRotation);
            }
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            var player = playerObject.GetComponent<TheGuy>();
            player.Move(moveX, moveY, targetRotation);
        }
    }
}
