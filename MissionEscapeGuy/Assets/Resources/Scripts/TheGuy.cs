using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using System;

public class TheGuy : NetworkBehaviour
{
    public static TheGuy Instance { get; private set; }
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    //private Bullet bullet;
    private Vector2 moveDirection;
    private float moveSpeed = 8;
    private float rotationSpeed = 128;
    private bool isPurple;
    private bool isBlue;
    private bool isRed;
    private bool isGreen;
    private bool isOrange;
    private const int MAX_HEALTH = 20;
    private const int MAX_SHIELD = 10;
    private int health;
    private int shield;
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    [SerializeField] private Sprite purpleSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private Sprite blankSprite;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = MAX_HEALTH;
        shield = MAX_SHIELD;

        body = body == null ? GetComponent<Rigidbody2D>() : body;
        animator = animator == null ? GetComponent<Animator>() : animator;
        spriteRenderer = spriteRenderer == null ? GetComponent<SpriteRenderer>() : spriteRenderer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float moveX, float moveY, Quaternion targetRotation)
    {
        SubmitPositionRequestServerRpc(moveX, moveY, targetRotation);
    }

    [Rpc(SendTo.Server)]
    public void SubmitPositionRequestServerRpc(float cX, float cY, Quaternion cRot, RpcParams rpcParams = default)
    {
        moveDirection = new Vector2(cX, cY).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, cRot, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        body.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void fire()
    {
        
    }

    private void initializeColor()
    {

    }

    public Vector2 getPosition()
    {
        return transform.position;
    }

    public Quaternion getRotation()
    {
        return transform.rotation;
    }

    public void takeDamage(int damage)
    {
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
