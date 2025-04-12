using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class TheGuy : MonoBehaviour
{
    public static TheGuy Instance { get; private set; }
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    //private Bullet bullet;
    private Vector2 moveDirection;
    private float moveX;
    private float moveY;
    private float moveSpeed = 8;
    private float rotationSpeed = 16;
    private bool isPurple;
    private bool isBlue;
    private bool isRed;
    private bool isGreen;
    private bool isOrange;
    private const int MAX_HEALTH = 20;
    private const int MAX_SHIELD = 10;
    private int health;
    private int shield;
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

        body = body == null ? body : GetComponent<Rigidbody2D>();
        animator = animator == null ? animator : GetComponent<Animator>();
        spriteRenderer = spriteRenderer == null ? spriteRenderer : GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
 
        checkDirection();
        SubmitNewPosition();
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        body.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void checkDirection()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - (Vector2) transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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

    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int heatlh)
    {
        this.health = health;
    }

    public int getShield()
    {
        return shield;
    }

    public void setShield()
    {
        this.shield = shield;
    }

    void SubmitNewPosition()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            var player = playerObject.GetComponent<HelloWorldPlayer>();
            player.Move();
        }
    }
}
