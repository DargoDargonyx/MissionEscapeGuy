using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform parentTransform;
    private Rigidbody2D bullet;
    public float bulletSpeed = 1.0f;
    private int bulletDamage;
    private float destroyDistance;
    private Vector2 initialPosition;
    private Vector2 currentPosition;
    private Vector2 direction = new(0,-1);
    private Vector2 destroyPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bullet = bullet == null ? GetComponent<Rigidbody2D>() : bullet;
        initialPosition = currentPosition = bullet.transform.position;
        destroyDistance = 50f;
        
        bullet.linearVelocity = direction;
        destroyPosition = new Vector2(direction.x + destroyDistance, direction.y + destroyDistance);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = bullet.transform.position;
        if (currentPosition.x >= destroyPosition.x || currentPosition.y >= destroyPosition.y)
        {
            Destroy(gameObject, 0.3f);
        }
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
        bullet.linearVelocity = direction.normalized;
    }

    public void setDamage(int bulletDamage)
    {
        this.bulletDamage = bulletDamage;
        destroyPosition = new Vector2(direction.x + destroyDistance, direction.y + destroyDistance);
    }
}
