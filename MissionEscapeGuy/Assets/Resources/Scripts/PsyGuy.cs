using Unity.Netcode;
using UnityEngine;

public class PsyGuy : NetworkBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private Bullet bullet;
    private TheGuy closestPlayer;
    private int health;
    private float moveSpeed = 1;
    private float attackRange = 10;
    private Vector2 targetPosition = new(0, 0);
    private Vector2 currentPosition;
    private float time;
    private float nextTime;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<NetworkObject>().Spawn();
        
        // animator = animator == null ? GetComponent<Animator>() : animator;
        body = body == null ? GetComponent<Rigidbody2D>() : body;
        bullet = bullet == null ? Resources.Load<Bullet>("Prefabs/Bullet") : bullet;

        health = 6;

        currentPosition = transform.position;
        time = nextTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        currentPosition = transform.position;
        closestPlayer = findClosestPlayer();

        if (Vector2.Distance(currentPosition, closestPlayer.transform.position) >= attackRange)
        {
            targetPortal();
        }
        else
        {
            targetPlayer(closestPlayer);
            if (time >= nextTime)
            {
                nextTime += 2f;
                shoot();
            }
        }
    }

    private void targetPlayer(TheGuy closestPlayer)
    {
        Vector2 direction = (Vector2) closestPlayer.transform.position - currentPosition;
        body.linearVelocity = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 128 * Time.deltaTime);
    }

    private void targetPortal()
    {
        body.linearVelocity = (targetPosition - currentPosition).normalized;
    }

    private TheGuy findClosestPlayer()
    {
        float closestDistance = 999f;
        ulong firstID = NetworkManager.Singleton.ConnectedClientsIds[0];
        NetworkObject firstPlayer = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(firstID);
        TheGuy closestPlayer = firstPlayer.GetComponent<TheGuy>();

        /* This loop iterates through every player in the GameManager and finds the closest. */
        foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
        {
            var playerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid);
            var currentPlayer = playerObject.GetComponent<TheGuy>();
            Vector2 PlayerPosition = currentPlayer.transform.position;
            float currentPlayerDistance = Vector2.Distance(currentPosition, PlayerPosition);

            if (currentPlayerDistance < closestDistance)
            {
                closestPlayer = currentPlayer;
                closestDistance = currentPlayerDistance;
            }
        }

        return closestPlayer;
    }

    private void shoot()
    {

    }
}
