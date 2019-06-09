// Created using the tutorials provided by Sebastian Lague on procedural landmass generation
// Videos 1 - 5 in the playlist @ https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
using UnityEngine;

// Generates textures
public static class TextureGenerator
{
    // Generate a coloured texture
    public static Texture2D TextureFromColourMap(Color[] colourMap, int xSize, int zSize)
    {
        // Create texture
        Texture2D texture = new Texture2D(xSize, zSize)
        {
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp
        };

        // Set the texture pixels and apply
        texture.SetPixels(colourMap);
        texture.Apply();

        // Return the texture
        return texture;
    }

    // Generates a hiegth map texture
    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        // The x and z size of the map
        int xSize = heightMap.GetLength(0);
        int zSize = heightMap.GetLength(1);

        // Creates a textures
        Texture2D texture = new Texture2D(xSize, zSize);

        // Creates a colour array
        Color[] colourMap = new Color[xSize * zSize];

        // Loop through all map positions
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // Lerp the colour depending on height of the generated noise map
                colourMap[z * xSize + x] = Color.Lerp(Color.black, Color.white, heightMap[x, z]);
            }
        }

        // Return the texture
        return TextureFromColourMap(colourMap, xSize, zSize);
    }
}
