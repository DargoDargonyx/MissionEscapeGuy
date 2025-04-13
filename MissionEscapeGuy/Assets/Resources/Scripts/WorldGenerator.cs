using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public Tile DeepWater;
    public Tile Water;
    public Tile Grass;
    public Tile Stone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tilemap tilemap = FindFirstObjectByType<Tilemap>();
        Vector3Int coord = tilemap.WorldToCell(transform.position);

        for (int x = 0; x < 255; x++) 
        {
            for (int y = 0; y < 255; y++)
            {
                int tx = x - 128;
                int ty = y - 128;
                coord.x = tx;
                coord.y = ty;

                double nx = (double) x / 255 + 0.5;
                double ny = (double) y / 255 + 0.5;

                double noise = (PerlinNoise.Noise(nx * 10, ny * 10) + 0.25 * PerlinNoise.Noise(nx * 80, ny * 80)) / 1.25;

                // Spawn area
                double dist = Math.Sqrt(Math.Pow(tx, 2) + Math.Pow(ty, 2));
                double diff = noise - 0.5;
                double adj = diff / (dist + 1);
                noise -= adj;

                // Edge of map
                // Rock wall
                double edgeDist = 128 - Math.Max(Math.Abs(tx), Math.Abs(ty));
                noise += 10 / Math.Pow(edgeDist, 2) * 5;


                if (noise < 0.2) 
                {
                    tilemap.SetTile(coord, DeepWater);
                }
                else if (noise < 0.4) 
                {
                    tilemap.SetTile(coord, Water);
                }
                else if (noise < 0.8) 
                {
                    tilemap.SetTile(coord, Grass);
                }
                else 
                {
                    tilemap.SetTile(coord, Stone);
                }
            }
        }
    }
}
