using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < 256; x++) 
        {
            for (int y = 0; y < 256; y++)
            {
                double nx = (x - 128) / 255 - 0.5;
                double ny = (y - 128) / 255 - 0.5;

                double noise = PerlinNoise.Noise(nx, ny);
            }
        }
    }
}
