using UnityEngine;

public class TortleGuy : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D body;
    private Bullet bullet;
    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private Vector2 targetDirection;
    private int health;
    private float time;
    private float nextTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 4;
        body = body == null ? GetComponent<Rigidbody2D>() : body;
        bullet = bullet == null ? Resources.Load<Bullet>("Prefabs/Bullet") : bullet;

        currentPosition = transform.position;
        targetPosition = new(0, 0); // World Origin, where spaceship is located
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void targetPlayer()
    {

    }

    private void damagePlayer()
    {

    }

    private Vector2? findNearestPlayer()
    {
        return new Vector2();
    }

    private Vector2 getDirection()
    {
        return new Vector2();
    }

    private bool isShooting()
    {
        return false;
    }
}
