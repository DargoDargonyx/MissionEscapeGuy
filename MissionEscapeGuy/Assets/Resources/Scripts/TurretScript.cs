using Unity.Netcode;
using UnityEngine;

public class TurretScript : NetworkBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Collider2D closestEnemy;

    public int level = 1;
    private float rotationSpeed;
    private float detectionRadius;
    private Bullet bullet;
    private float time;
    private float nextTime;
    private Quaternion targetRotation;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Sprite levelOneSprite;
    [SerializeField] private Sprite levelTwoSprite;
    [SerializeField] private Sprite levelThreeSprite;
    [SerializeField] private Transform launchOffset;

    void Start()
    {
        if (MasterController.isHost)
        {
            gameObject.GetComponent<NetworkObject>().Spawn();
        }

        body = body == null ? GetComponent<Rigidbody2D>() : body;
        spriteRenderer = spriteRenderer == null ? GetComponent<SpriteRenderer>() : spriteRenderer;
        bullet = bullet == null ? Resources.Load<Bullet>("Prefabs/Bullet") : bullet;

        time = nextTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterController.isHost)
        {
            checkLevelConstraints();
            findClosestEnemy();
            checkDirection();
            fire();

            time += Time.deltaTime;
        }
    }

    private void checkLevelConstraints()
    {
        switch (level)
        {
            case 1:
                spriteRenderer.sprite = levelOneSprite;
                rotationSpeed = 15f;
                detectionRadius = 5f;
                break;
            case 2:
                spriteRenderer.sprite = levelTwoSprite;
                rotationSpeed = 20f;
                detectionRadius = 8f;
                break;
            case 3:
                spriteRenderer.sprite = levelThreeSprite;
                rotationSpeed = 25f;
                detectionRadius = 12f;
                break;
            default:
                break;
        }
    }

    private void checkDirection()
    {
        Vector2 direction = closestEnemy.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void findClosestEnemy()
    {
        Collider2D[] hitboxes = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D hitbox in hitboxes)
        {
            float distance = Vector2.Distance(transform.position, hitbox.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = hitbox;
            }
        }
    }

    private void fire()
    {
        //Quaternion newRot = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90f));

        if (time >= nextTime)
        {
            nextTime += 1f;
            Instantiate(bullet, launchOffset.position, launchOffset.rotation);
        }
    }

}
