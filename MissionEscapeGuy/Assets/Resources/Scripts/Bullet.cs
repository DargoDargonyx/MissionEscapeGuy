using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform parentTransform;
    private Rigidbody2D bullet;
    public float bulletSpeed = 1.0f;
    private float destroyDistance;
    private Vector2 initialPosition;
    private Vector2 currentPosition;
    private Vector2 direction;
    private Vector2 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bullet = bullet == null ? GetComponent<Rigidbody2D>() : bullet;
        initialPosition = currentPosition = bullet.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = bullet.transform.position;
    }

    public void setDirection(Vector2 direction)
    {
        bullet.linearVelocity = direction.normalized;
    }
}
