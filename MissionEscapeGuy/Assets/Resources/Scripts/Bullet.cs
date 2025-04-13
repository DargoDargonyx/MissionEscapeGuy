using System;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private Rigidbody2D bullet;
    public LayerMask enemyLayer;
    private SpriteRenderer spriteRenderer;
    public float bulletSpeed = 15f;
    private int bulletDamage;
    private float destroyDistance;
    private Vector2 initialPosition;
    private Vector2 currentPosition;

    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite redSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MasterController.isHost) {
            gameObject.GetComponent<NetworkObject>().Spawn();

            bullet = bullet == null ? GetComponent<Rigidbody2D>() : bullet;
            spriteRenderer = spriteRenderer == null ? GetComponent<SpriteRenderer>() : spriteRenderer;

            initialPosition = currentPosition = bullet.transform.position;
            destroyDistance = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterController.isHost)
        {
            transform.position += transform.right * Time.deltaTime * bulletSpeed;
            currentPosition = bullet.transform.position;
            
            if (Math.Pow(currentPosition.x, 2) - Math.Pow(initialPosition.x, 2) >= Math.Pow(destroyDistance, 2) || Math.Pow(currentPosition.y, 2) - Math.Pow(initialPosition.y, 2) >= Math.Pow(destroyDistance, 2))
            {
                Destroy(gameObject, 0.3f);
            }
        }
    }

    public void setDamage(int bulletDamage)
    {
        this.bulletDamage = bulletDamage;
    }

    public void setColor()
    {
        if (gameObject.CompareTag("Player") || gameObject.CompareTag("Turret"))
        {
            spriteRenderer.sprite = blueSprite;
        }
        else
        {
            spriteRenderer.sprite = redSprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (MasterController.isHost)
        {
            GameObject collisionObject = collision.gameObject;
            string tag = collisionObject.tag;

            switch (tag)
            {
                case "TortleGuy":
                    TortleGuy tortleGuy = collisionObject.GetComponent<TortleGuy>();
                    tortleGuy.takeDamage(2f);
                    break;
                case "PsyGuy":
                    PsyGuy psyGuy = collisionObject.GetComponent<PsyGuy>();
                    psyGuy.takeDamage(2f);
                    break;
                case "BigBack":
                    BigBack bigBack = collisionObject.GetComponent<BigBack>();
                    bigBack.takeDamage(2f);
                    break;
                case "Player":
                    TheGuy player = collisionObject.GetComponent<TheGuy>();
                    player.takeDamage(2);
                    break;
                case "Spaceship":
                    Spaceship spaceship = collisionObject.GetComponent<Spaceship>();
                    spaceship.takeDamage(2f);
                    break;
                default:
                    break;
            }

            Destroy(gameObject, 0.3f);
        }
    }
}
