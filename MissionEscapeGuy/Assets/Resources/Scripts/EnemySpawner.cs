using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 5f;
    [SerializeField] int maxSpawnLimit = 20;
    public GameObject turtle;
    public GameObject bigBack;
    public GameObject psyGuy;
    public GameObject turret;
    private Tilemap tilemap;
    private BoxCollider2D collider;
    private float timeUntilSpawn;
    public static int numEnemies = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        collider = collider == null ? GetComponent<BoxCollider2D>() : collider;
        setTimeUntilSpawn();
        tilemap = FindFirstObjectByType<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0 && numEnemies <= maxSpawnLimit)
        {
            spawnEnemy();
            setTimeUntilSpawn();
        }
    }

    private void spawnEnemy()
    {
        Vector2 randomEdgePosition = getRandomEdgePosition();
        if (!pointIsRock(randomEdgePosition))
        {
            int roll = new System.Random().Next(3);
            GameObject enemyToSpawn;
            switch (roll)
            {
                case 0: enemyToSpawn = turtle; break;
                case 1: enemyToSpawn = bigBack; break;
                case 2: enemyToSpawn = psyGuy; break;
                default: enemyToSpawn = turret; break;
            }
            Instantiate(enemyToSpawn, randomEdgePosition, Quaternion.identity);
            numEnemies += 1;
            Debug.Log("Enemy Spawned!");
        }
        else
        {
            Debug.Log("Enemy Not Spawned!");
        }
    }

    private void setTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private Vector2 getRandomEdgePosition()
    {
        float randomX = Random.Range(-220f / 2f, 220f / 2f);
        float randomY = Random.Range(-220f / 2f, 220f / 2f);
        Vector2 randomPosition = new(randomX, randomY);

        return collider.ClosestPoint(randomPosition);
    }

    private bool pointIsRock(Vector2 point)
    {
        Vector3Int newPoint = tilemap.WorldToCell(point);
        Tile spawnTile = tilemap.GetTile<Tile>(newPoint);

        return spawnTile.name == "Tile_3";
    }
}
