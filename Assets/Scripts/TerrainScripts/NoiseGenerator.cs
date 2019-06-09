// Created using the tutorials provided by Sebastian Lague on procedural landmass generation
// Videos 1 - 5 in the playlist @ https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
using UnityEngine;

// Noise generator
public class NoiseGenerator
{
    // Static function to generate noise map
    public static float[,] GenerateNoiseMap(int xSize, int zSize, int seed, float scale, float lacunarity, float persist, int octaves, Vector2 offset)
    {
        // Creat the noise map
        float[,] data = new float[xSize, zSize];

        // Random generation
        System.Random random = new System.Random(seed);

        // Octave offsets
        Vector2[] octaveOffsets = new Vector2[octaves];

        // Loop through the octaves
        for (int i = 0; i < octaves; i++)
        {
            // Random offsets for octaves
            float offsetX = random.Next(-100000, 100000) + offset.x;
            float offsetY = random.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // Clamp scale
        if (scale <= 0) scale = 0.0001f;

        // Clamp min and max values
        float maxHeight = float.MinValue;
        float minHeight = float.MaxValue;

        //
        float halfWidth = xSize / 2.0f;
        float halfHeight = zSize / 2.0f;

        // Lopp through positions
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // The amplitude and frequency of the noise
                float amplitude = 1.0f;
                float frequency = 1.0f;
                float height = 0.0f;

                // Loop through the octaves
                for (int i = 0; i < octaves; i++)
                {
                    // Noise sampling - add random sampling 
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (z - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    // Generate noise
                    float perlin = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    height += perlin * amplitude;

                    // Multiply amplitude by persistence
                    amplitude *= persist;

                    // Multiply frequency by lacunarity
                    frequency *= lacunarity;
                }

                // Clamp min and max values
                if      (height > maxHeight) maxHeight = height;
                else if (height < minHeight) minHeight = height;

                // Set the noise map height value
                data[x, z] = height;
            }
        }

        // Loop through positions
        for (int z = 0; z < zSize; z++)
            for (int x = 0; x < xSize; x++)

                // Normalises the data
                data[x, z] = Mathf.InverseLerp(minHeight, maxHeight, data[x, z]);

        // Return the noise
        return data;
    }
}