using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap GTM;
    //public Tilemap STM;
    public Tile GT1;
    public Tile GT2;
    public Tile GT3;
    //public Tile ST1;

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
                        GTM.SetTile(coord, GT1);
                    break;
                    case 1:
                        GTM.SetTile(coord, GT2);
                    break;
                    case 2: 
                        GTM.SetTile(coord, GT3);
                    continue;
                    case 3: 
                        //STM.SetTile(coord, ST1);
                    break;
                }
            }
        }
    }
}
