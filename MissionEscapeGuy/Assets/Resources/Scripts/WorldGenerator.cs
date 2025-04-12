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
                coord.x = x - 128;
                coord.y = y - 128;

                double nx = (double) x / 255 - 0.5;
                double ny = (double) y / 255 - 0.5;

                double noise = PerlinNoise.Noise(nx * 20, ny * 20);

                int tile = (int) Math.Floor(noise * 4);

                switch (tile) 
                {
                    case 0: 
                        tilemap.SetTile(coord, DeepWater);
                    break;
                    case 1:
                        tilemap.SetTile(coord, Water);
                    break;
                    case 2: 
                        tilemap.SetTile(coord, Grass);
                    break;
                    case 3: 
                        tilemap.SetTile(coord, Stone);
                    break;
                }
            }
        }
    }
}
