using Unity.VisualScripting;
using UnityEngine;

public class TheGuy : MonoBehaviour
{
    public static TheGuy Instance { get; private set; }
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Bullet bullet;
    private Vector2 moveDirection;
    private float moveX;
    private float moveY;
    private float moveSpeed;
    private bool isPurple;
    private bool isBlue;
    private 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
