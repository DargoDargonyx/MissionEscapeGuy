using UnityEngine;
using UnityEngine.UI;

public class BigBack : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D body;
    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private Vector2 targetDirection;
    private TheGuy closestPlayer;
    private const float MAX_HEALTH = 10f;
    private float health = MAX_HEALTH;
    private float time;
    private float nextTime;
    private float attackRange;
    private int attackDamage;

    [SerializeField] private EnemyHealthBarScript healthBar;
    [SerializeField] private Scrollbar scrollBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = body == null ? GetComponent<Rigidbody2D>() : body;

        currentPosition = transform.position;
        attackRange = 10f;
        attackDamage = 4;

        targetPosition = new(0, 0); // World Origin, where spaceship is located
        targetDirection = currentPosition - targetPosition;
        
        time = Time.time;
        nextTime = time;

        healthBar = healthBar == null ? GetComponentInChildren<EnemyHealthBarScript>() : healthBar;
    }

    // Update is called once per frame
    void Update()
    {
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
            time = nextTime;
            nextTime += 2f;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Spaceship")) && time >= nextTime)
        {
            TheGuy otherObject = collision.gameObject.GetComponent<TheGuy>();
            time = nextTime;
            nextTime += 2f;
            otherObject.takeDamage(attackDamage);
        }
    }

    private void targetPortal()
    {
        Vector2 direction = (targetPosition - currentPosition).normalized;
        body.linearVelocity = direction.normalized * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 128 * Time.deltaTime);
    }

    private void targetPlayer(TheGuy closestPlayer)
    {
        Vector2 direction = (Vector2) closestPlayer.transform.position - currentPosition;
        body.linearVelocity = direction.normalized * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 128 * Time.deltaTime);
    }

    private TheGuy findNearestPlayer()
    {
        return FindFirstObjectByType<TheGuy>();
    }

    private void checkDeath()
    {
        if (health <= 0)
        {
            scrollBar.handleRect.gameObject.SetActive(false);
            Destroy(gameObject, 0.25f);
        }
            
    }


    public void takeDamage(float damage)
    {
        if (health > damage)
        {
            health -= damage;
            healthBar.UpdateHealthBar(health, MAX_HEALTH);
        }
        else
        {
            health = 0;
        }
    }
}
