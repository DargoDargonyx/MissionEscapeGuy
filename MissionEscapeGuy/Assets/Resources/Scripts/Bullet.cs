using System;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private Rigidbody2D bullet;
    public float bulletSpeed = 10f;
    private int bulletDamage;
    private float destroyDistance;
    private Vector2 initialPosition;
    private Vector2 currentPosition;
    [SerializeField] private LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<NetworkObject>().Spawn();

        bullet = bullet == null ? GetComponent<Rigidbody2D>() : bullet;
        initialPosition = currentPosition = bullet.transform.position;
        destroyDistance = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * bulletSpeed;
        currentPosition = bullet.transform.position;
        
        if (Math.Pow(currentPosition.x, 2) - Math.Pow(initialPosition.x, 2) >= Math.Pow(destroyDistance, 2) || Math.Pow(currentPosition.y, 2) - Math.Pow(initialPosition.y, 2) >= Math.Pow(destroyDistance, 2))
        {
            Destroy(gameObject, 0.3f);
        }
    }

    public void setDamage(int bulletDamage)
    {
        this.bulletDamage = bulletDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        Destroy(gameObject, 0.3f);
    }
}
