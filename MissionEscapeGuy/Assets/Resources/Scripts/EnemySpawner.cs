using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private float minSpawnTime = 0.5f;
    [SerializeField] private float maxSpawnTime = 1f;
    [SerializeField] int maxSpawnLimit = 2000;
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
            int roll = new System.Random().Next(20);
            GameObject enemyToSpawn;
            switch (roll)
            {
                case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: enemyToSpawn = turtle; break;
                case 11: case 12: case 13: case 14: enemyToSpawn = bigBack; break;
                case 15:  case 16: case 17: case 18: case 19: case 20: enemyToSpawn = psyGuy; break;
                default: enemyToSpawn = turtle; break;
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
