using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TortleGuy : NetworkBehaviour
{
    public float moveSpeed = 4f;
    private Rigidbody2D body;
    private Animator animator;
    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private Vector2 targetDirection;
    private TheGuy closestPlayer;
    private const float MAX_HEALTH = 4f; 
    private NetworkVariable<float> health = new NetworkVariable<float>(MAX_HEALTH);
    private float time;
    private float nextTime;
    private float attackRange = 10f;

    [SerializeField] EnemyHealthBarScript healthBar;
    [SerializeField] private Scrollbar scrollBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MasterController.isHost)
        {
            gameObject.GetComponent<NetworkObject>().Spawn();
            
            body = body == null ? GetComponent<Rigidbody2D>() : body;
            animator = animator == null ? GetComponent<Animator>() : animator;

            currentPosition = transform.position;

            targetPosition = new(0, 0); // World Origin, where spaceship is located
            targetDirection = currentPosition - targetPosition;
            
            time = Time.time;
            nextTime = time;
        }

        healthBar = healthBar == null ? GetComponentInChildren<EnemyHealthBarScript>() : healthBar;
    }

    // Update is called once per frame
    void Update()
    {
        checkMovement();
        if (MasterController.isHost) {
            checkDeath();

            time += Time.deltaTime;

            currentPosition = transform.position;

            closestPlayer = findNearestPlayer();
            if (Vector2.Distance(currentPosition, closestPlayer.transform.position) <= attackRange)
            {
                targetPlayer(closestPlayer);
            }
            else
            {
                targetPortal();
            }
        }
        healthBar.UpdateHealthBar(health.Value, MAX_HEALTH);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (MasterController.isHost && collision.gameObject.CompareTag("Player") && time >= nextTime)
        {
            time = Time.time;
            nextTime = time + 1f;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        animator.SetBool("isAttacking", true);
        if (MasterController.isHost && collision.gameObject.CompareTag("Player") && time >= nextTime)
        {
            nextTime += 1f;
            TheGuy otherObject = collision.gameObject.GetComponent<TheGuy>();
            otherObject.takeDamage(1);
        }
        animator.SetBool("isAttacking", false);
    }

    private void targetPortal()
    {
        Vector2 direction = (targetPosition - currentPosition).normalized;
        body.linearVelocity = direction.normalized * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 128 * Time.deltaTime);
    }

    private void targetPlayer(TheGuy closestPlayer)
    {
        Vector2 direction = (Vector2) closestPlayer.transform.position - currentPosition;
        body.linearVelocity = direction.normalized * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 128 * Time.deltaTime);
    }

    private TheGuy findNearestPlayer()
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

    private Vector2 getDirection()
    {
        return new Vector2();
    }

    public void takeDamage(float damage)
    {
        if (MasterController.isHost)
        {
            if (health.Value > damage)
            {
                health.Value -= damage;
            }
            else
            {
                health.Value = 0;
            }
            Debug.Log(health);
        }
    }

    private void checkDeath()
    {
        if (health.Value == 0)
        {
            scrollBar.handleRect.gameObject.SetActive(false);
            Destroy(gameObject, 0.25f);
        }
    }

    private void checkMovement()
    {
        if (body.linearVelocity.x > 0 || body.linearVelocity.y > 0)
        {
            animator.SetBool("isMoving", true);
        }
    }

}
