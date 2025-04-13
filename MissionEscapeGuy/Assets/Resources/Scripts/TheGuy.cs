using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.Tilemaps;
using UnityEditor.U2D;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.Collections;

public class TheGuy : MonoBehaviour
{
    public static TheGuy Instance { get; private set; }
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Bullet bullet;
    private Vector2 moveDirection;
    private float moveSpeed = 8;
    private float rotationSpeed = 128;
    private bool isPurple;
    private bool isBlue;
    private bool isRed;
    private bool isGreen;
    private bool isOrange;
    private float regenTimer = 10;
    private const int MAX_HEALTH = 20;
    private const int MAX_SHIELD = 10;
    private int health = MAX_HEALTH;
    private int shield = MAX_SHIELD;
    private float moveX;
    private float moveY;
    Quaternion targetRotation;
    public Camera camera;
    [SerializeField] private Transform launchOffset;
    [SerializeField] private Sprite purpleSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private Sprite blankSprite;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = body == null ? GetComponent<Rigidbody2D>() : body;
        animator = animator == null ? GetComponent<Animator>() : animator;
        spriteRenderer = spriteRenderer == null ? GetComponent<SpriteRenderer>() : spriteRenderer;
        bullet = bullet == null ? Resources.Load<Bullet>("Prefabs/Bullet") : bullet;
    }

    // Update is called once per frame
    void Update()
    {
        regenTimer -= Time.deltaTime;
        if (regenTimer < 0)
        {
            setShield(Math.Clamp(getShield() + 1, -1, MAX_SHIELD));
            regenTimer = 10;
        }
        if (health == 0)
            Destroy(gameObject);

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        Vector3 newCameraPos = new Vector3(transform.position.x, transform.position.y, -10f);
        camera.transform.position = newCameraPos;

        checkDirection();
        Move(moveX, moveY, targetRotation);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    private void checkDirection()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - (Vector2) transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    public void Move(float moveX, float moveY, Quaternion targetRotation)
    {
        moveDirection = new Vector2(moveX, moveY).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Tilemap tilemap = FindFirstObjectByType<Tilemap>();
        Tile tile = tilemap.GetTile<Tile>(tilemap.WorldToCell(transform.position));
        float speedMod = 1;
        if (tile.name != "Tiles_0") {
            speedMod = tile.name == "Tiles_1" ? 0.5f : 0.25f;
        }
        body.linearVelocity = new Vector2(moveDirection.x * moveSpeed * speedMod, moveDirection.y * moveSpeed * speedMod);
    }

    public void Fire()
    {
        Bullet clone = Instantiate(bullet, launchOffset.position, transform.rotation);
        clone.setEnemyStatus(false);
    }

    public Vector2 getPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public Vector2 getRotation()
    {
        return new Vector2(transform.rotation.x, transform.rotation.y);
    }

    public void takeDamage(int damage)
    {
        Debug.Log("Initial Health: " + getHealth());
        Debug.Log("Initial Shield: " + getShield());
        // difference will be negative if damage is more than
        // the amount of shields the player has.
        int difference = shield - damage;

        if (difference < 0)
        {
            difference *= -1;
            setShield(0);
            setHealth(health - difference);
        }
        else
        {
            setShield(shield - damage);
        }
        Debug.Log("New Health: " + getHealth());
        Debug.Log("New Shield: " + getShield());
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int health)
    {
        if (health < 0) return;
        this.health = health;
    }

    public int getShield()
    {
        return shield;
    }

    public void setShield(int shield)
    {
        if (shield < 0) return;
        this.shield = shield;
    }
}
