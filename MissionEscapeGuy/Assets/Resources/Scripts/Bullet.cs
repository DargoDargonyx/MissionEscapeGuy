using System;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private Transform parentTransform;
    private Rigidbody2D bullet;
    public float bulletSpeed = 2.0f;
    private int bulletDamage;
    private float destroyDistance;
    private Vector2 initialPosition;
    private Vector2 currentPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<NetworkObject>().Spawn();

        bullet = bullet == null ? GetComponent<Rigidbody2D>() : bullet;
        initialPosition = currentPosition = bullet.transform.position;
        destroyDistance = 50f;
    }

    // Update is called once per frame
    void Update()
    {
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
}
