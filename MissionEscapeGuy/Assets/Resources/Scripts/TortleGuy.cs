using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TortleGuy : MonoBehaviour
{
    public float moveSpeed = 4f;
    private Rigidbody2D body;
    private Animator animator;
    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private Vector2 targetDirection;
    private TheGuy closestPlayer;
    private const float MAX_HEALTH = 4f; 
    private float health = MAX_HEALTH;
    private float time;
    private float nextTime;
    private float attackRange = 10f;

    [SerializeField] EnemyHealthBarScript healthBar;
    [SerializeField] private Scrollbar scrollBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
{
        body = body == null ? GetComponent<Rigidbody2D>() : body;
        animator = animator == null ? GetComponent<Animator>() : animator;

        currentPosition = transform.position;

        targetPosition = new(0, 0); // World Origin, where spaceship is located
        targetDirection = currentPosition - targetPosition;
        
        time = Time.time;
        nextTime = time;

        healthBar = healthBar == null ? GetComponentInChildren<EnemyHealthBarScript>() : healthBar;
    }

    // Update is called once per frame
    void Update()
    {
        checkMovement();
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
        healthBar.UpdateHealthBar(health, MAX_HEALTH);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Spaceship")) && time >= nextTime)
        {
            time = Time.time;
            nextTime = time + 1f;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        animator.SetBool("isAttacking", false);
        if (collision.gameObject.CompareTag("Player") && time >= nextTime)
        {
            animator.SetBool("isAttacking", true);
            time = Time.time;
            nextTime = time + 1f;
            TheGuy otherObject = collision.gameObject.GetComponent<TheGuy>();
            otherObject.takeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Spaceship") && time >= nextTime)
        {
            animator.SetBool("isAttacking", true);
            time = Time.time;
            nextTime = time + 1f;
            Spaceship otherObject = collision.gameObject.GetComponent<Spaceship>();
            otherObject.takeDamage(1);
        }
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
        return FindFirstObjectByType<TheGuy>();   
    }

    private Vector2 getDirection()
    {
        return new Vector2();
    }

    public void takeDamage(float damage)
    {
        if (health > damage)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        Debug.Log(health);
    }

    private void checkDeath()
    {
        if (health == 0)
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
