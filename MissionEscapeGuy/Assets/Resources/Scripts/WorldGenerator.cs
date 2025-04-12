using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap tileMap;
    public Tile DeepWater;
    public Tile Water;
    public Tile Grass;
    //public Tile Stone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (byte x = 0; x < 255; x++) 
        {
            for (byte y = 0; y < 255; y++)
            {
                Vector3Int coord = new Vector3Int(x - 128, y - 128);
                double nx = x / 255 - 0.5;
                double ny = y / 255 - 0.5;

                double noise = PerlinNoise.Noise(nx, ny);

                byte tile = (byte)((byte) noise * 4);

                switch (tile) 
                {
                    case 0: 
                        tileMap.SetTile(coord, DeepWater);
                    break;
                    case 1:
                        tileMap.SetTile(coord, Water);
                    break;
                    case 2: 
                        tileMap.SetTile(coord, Grass);
                    continue;
                    case 3: 
                        //STM.SetTile(coord, ST1);
                    break;
                }
            }
        }
    }
}
