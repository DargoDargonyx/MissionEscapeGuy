using UnityEngine;

public class PsyGuy : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private Bullet bullet;
    private TheGuy closestPlayer;
    private const float MAX_HEALTH = 6f;
    private float health = MAX_HEALTH;
    private float moveSpeed = 2f;
    private float attackRange = 10f;
    private Vector2 targetPosition = new(0, 0);
    private Vector2 currentPosition;
    private float time;
    private float nextTime;
    [SerializeField] private EnemyHealthBarScript healthBar;
    [SerializeField] private Transform launchOffset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = body == null ? GetComponent<Rigidbody2D>() : body;
        bullet = bullet == null ? Resources.Load<Bullet>("Prefabs/Bullet") : bullet;

        healthBar.UpdateHealthBar(health, MAX_HEALTH);

        currentPosition = transform.position;
        time = nextTime = Time.time;

        healthBar = healthBar == null ? GetComponentInChildren<EnemyHealthBarScript>() : healthBar;
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();

        time += Time.deltaTime;
        currentPosition = transform.position;
        closestPlayer = findClosestPlayer();

        if (Vector2.Distance(currentPosition, closestPlayer.transform.position) >= attackRange)
        {
            targetPortal();
            if (time >= nextTime && Vector2.Distance(currentPosition, targetPosition) <= attackRange)
            {
                time = Time.time;
                nextTime = time + 2f;
                shoot();
            }
        }
        else
        {
            targetPlayer(closestPlayer);
            if (time >= nextTime)
            {
                time = Time.time;
                nextTime = time + 2f;
                shoot();
            }
        }
        healthBar.UpdateHealthBar(health, MAX_HEALTH);
    }

    private void targetPlayer(TheGuy closestPlayer)
    {
        Vector2 direction = (Vector2) closestPlayer.transform.position - currentPosition;
        body.linearVelocity = direction.normalized * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 128 * Time.deltaTime);
    }

    private void targetPortal()
    {
        Vector2 direction = (targetPosition - currentPosition).normalized;
        body.linearVelocity = direction.normalized * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 128 * Time.deltaTime);
    }

    private TheGuy findClosestPlayer()
    {
        return FindFirstObjectByType<TheGuy>();
    }

    private void shoot()
    {
        Bullet clone = Instantiate(bullet, launchOffset.position, launchOffset.rotation);
        clone.setEnemyStatus(true);
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
            health = 0f;
        }
    }

    private void checkDeath()
    {
        if (health == 0)
        {
            healthBar.UpdateHealthBar(0.000001f, MAX_HEALTH);
            Destroy(gameObject, 0.25f);
        }
    }
}
