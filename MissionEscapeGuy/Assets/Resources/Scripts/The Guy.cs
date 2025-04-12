using Unity.VisualScripting;
using UnityEngine;

public class TheGuy : MonoBehaviour
{
    public static TheGuy Instance { get; private set; }
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    //private Bullet bullet;
=======
    // private Bullet bullet;
>>>>>>> Stashed changes
=======
    // private Bullet bullet;
>>>>>>> Stashed changes
    private Vector2 moveDirection;
    private float moveX;
    private float moveY;
    private float moveSpeed;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FedUpdate()
    {
        
    }

    private void checkDirection()
    {

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
}
