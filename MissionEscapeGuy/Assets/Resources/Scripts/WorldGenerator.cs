using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public Tile DeepWater;
    public Tile Water;
    public Tile Grass;
    public Tile Stone;
    public TileBase[][] tiledata;

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

                double nx = (double) x / 255 + 0.5;
                double ny = (double) y / 255 + 0.5;

                double noise = PerlinNoise.Noise(nx * 20, ny * 20);

                if (noise < 0.25) 
                {
                    tilemap.SetTile(coord, DeepWater);
                }
                else if (noise < 0.5) 
                {
                    tilemap.SetTile(coord, Water);
                }
                else if (noise < 0.75) 
                {
                    tilemap.SetTile(coord, Grass);
                }
                else 
                {
                    tilemap.SetTile(coord, Stone);
                }
                
                tiledata[x][y] = tilemap.GetTile(coord);
            }
        }

        tilemap.GetComponent<TileSync>().tilegrid
    }
}
