using UnityEngine;
using Unity.Netcode;

public class ClientManager : MonoBehaviour
{
    NetworkObject player;
    TheGuy playerScript;
    private float moveX;
    private float moveY;
    Quaternion targetRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        playerScript = player.GetComponent<TheGuy>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        checkDirection();
        SubmitNewPosition();
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
        if (NetworkManager.Singleton.IsClient)
        {
            Move();
        }
    }

    public void Move()
    {
        playerScript.SubmitPositionRequestServerRpc(moveX, moveY, targetRotation);
    }
}
