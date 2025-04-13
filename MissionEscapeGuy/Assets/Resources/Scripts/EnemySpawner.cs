using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 5f;
    public Tile rock;
    private Tilemap tilemap;
    private BoxCollider2D collider;
    private float timeUntilSpawn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        collider = collider == null ? GetComponent<BoxCollider2D>() : collider;
        setTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
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
            Instantiate(enemyToSpawn, randomEdgePosition, Quaternion.identity);
            Debug.Log("Turtle Spawned!");
        }
        else
        {
            Debug.Log("Turtle Not Spawned!");
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

        return (spawnTile.name == "Tile_0");
    }
}
